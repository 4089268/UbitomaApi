using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

using RutasApi.Data;
using RutasApi.Models;
using RutasApi.Models.Entities.Arquos;
using RutasApi.Models.Entities.ArquosMedia;
using RutasApi.Models.Entities.Sicem;
using RutasApi.Models.Request;
using RutasApi.Models.Responses;
using Microsoft.Extensions.Logging;

namespace RutasApi.Services {
    public class ArquosService {

        private readonly ILogger<ArquosService> logger;
        public ArquosService(ILogger<ArquosService> logger){
            this.logger = logger;
        }

        public DatosEmpresa? ObtenerDatosEmpres(Ruta oficina){
            var response = new DatosEmpresa();
            try {
                using(var sqlConnection = new SqlConnection(oficina.ConnectionString)){
                    sqlConnection.Open();
                    var _query = "Select  nombre_comercial, razon_social, direccion, colonia, ciudad, estado, rfc From [Global].[Cat_Empresa] ";
                    using(var _command = new SqlCommand(_query, sqlConnection)){
                        using(SqlDataReader _reader = _command.ExecuteReader()){
                            if(_reader.Read()){
                                response.Nombre_Comercial    = _reader["nombre_comercial"].ToString();
                                response.Razon_Social        = _reader["razon_social"].ToString();
                                response.Direccion           = _reader["direccion"].ToString();
                                response.Colonia             = _reader["colonia"].ToString();
                                response.Ciudad              = _reader["ciudad"].ToString();
                                response.Estado              = _reader["estado"].ToString();
                                response.Rfc                 = _reader["rfc"].ToString();
                            }
                        }
                    }
                    sqlConnection.Close();
                }
                return response;
            }catch(Exception err){
                Console.WriteLine($">> Error al tratar de obtener los datos de la oficina {oficina.Oficina}\n\tError:{err.Message}\n\tStacktrace:{err.StackTrace}");
                return null;
            }
        }
        
        public IEnumerable<CuentaModel>? ObtenerPadron(Ruta oficina, int subsistema = 0, int sector = 0, int manzana = 0, bool omitirUbicados = true){
            try{
                // * Obtener el padron
                List<VwCatPadron> datosPadron = new List<VwCatPadron>();
                using(var dbContext = new ArquosContext(oficina.ConnectionString)){
                    IQueryable<VwCatPadron> _datosPadron;

                    if( omitirUbicados){
                        _datosPadron = dbContext.VwCatPadrons.Where( item => (item.Latitud == 0 || item.Latitud == null) && (item.Longitud == 0 || item.Longitud == null) );
                    }else{
                        _datosPadron = dbContext.VwCatPadrons;
                    }

                    //*** filtrat por subsistema, sector y manzana
                    if( subsistema > 0){
                        _datosPadron = _datosPadron.Where(item => item.Sb == subsistema);
                    }
                    if( sector > 0){
                        _datosPadron = _datosPadron.Where(item => item.Sector == sector);
                    }
                    if( manzana > 0){
                        _datosPadron = _datosPadron.Where(item => item.Manzana == manzana);
                    }

                    datosPadron = _datosPadron.ToList();
                }


                // * generar lista de cuentasModel
                var cuentasModel = datosPadron.Select( item => new CuentaModel(item)).ToList<CuentaModel>();
                return cuentasModel;
            }catch(Exception err){
                logger.LogError(err, "Error al obtener el padron de la oficina {oficina}: {message}", oficina.Oficina, err.Message);
                return null;
            }
        }

        public IEnumerable<CuentaModel>? ObtenerPadron(Ruta oficina, IEnumerable<int> accounts){
            try {
            
                // * get the padron
                var datosPadron = new List<VwCatPadron>();
                using(var dbContext = new ArquosContext(oficina.ConnectionString)){
                    datosPadron = dbContext.VwCatPadrons
                        .Where( element => accounts.Contains((int)element.IdCuenta))
                        .ToList();
                }

                // * cast data as CuentaModel
                var cuentasModel = datosPadron.Select( item => new CuentaModel(item)).ToList<CuentaModel>();

                return cuentasModel;

            }catch(Exception err){
                logger.LogError(err, "Error al obtener el padron de la oficina {oficina}: {message}", oficina.Oficina, err.Message);
                return null;
            }
        }

        public List<RutasActualizacionResponse> ActualizarPadron(Ruta oficina, List<RutasActualizarItemRequest> cuentas ){
            var result = new List<RutasActualizacionResponse>();
            foreach(var _cuenta in cuentas){

                var _tempResp = new RutasActualizacionResponse(){
                    Id_Cuenta = _cuenta.Id_Cuenta,
                    Id_Padron = _cuenta.Id_Padron
                };

                // Actualizar la localizacion
                var _tmpLat = double.TryParse(_cuenta.Latitud.ToString(),out double tmpLa)?tmpLa:0;
                var _tmpLon = double.TryParse(_cuenta.Longitud.ToString(),out double tmpLo)?tmpLo:0;
                if(_tmpLat != 0 && _tmpLon != 0){
                    ActualizarLocalizacion(oficina, Convert.ToInt64(_cuenta.Id_Padron), _tmpLat, _tmpLon);
                }

                // Agregar observaciones 
                if(_cuenta.Observaciones.Trim().Length > 1){
                    int idCuenta = int.TryParse(_cuenta.Id_Cuenta, out int tmpC)?tmpC:0;
                    int idPadron = int.TryParse(_cuenta.Id_Padron, out int tmpP)?tmpP:0;
                    AgregarObservaciones(oficina, idPadron, idCuenta, string.Format("[UBITOMA] - {0}", _cuenta.Observaciones));
                }

                // Actualizar los valores de los catalogo sugeridos
                if(_cuenta.Id_Estatus > 0){
                    // Actualizar id estatus
                }
                if(_cuenta.Id_Tarifa > 0){
                    // Actualizar id tarifa
                }
                if(_cuenta.Id_Situacion > 0){
                    // Actualizar id situacion toma
                }
                if(_cuenta.Id_Giro > 0){
                    // Actualizar id giro
                }
                if(_cuenta.Id_AnomaliaPredio > 0){
                    // Actualizar Id_AnomaliaPredio
                }

                _tempResp.Actualizado = 1;
                _tempResp.Mensaje = "Actualizado";


                result.Add(_tempResp);
            }
            return result;
        }

        /// <summary>
        /// updated localization and comments on the arquos-db-padron
        /// </summary>
        /// <param name="oficina"></param>
        /// <param name="request"></param>
        public void ActualizarPadronV2(Ruta oficina, CapturaRequest request){
            var result = new List<RutasActualizacionResponse>();

            var _tempResp = new RutasActualizacionResponse(){
                Id_Cuenta = request.CodigoCuenta.ToString(),
                Id_Padron = request.IdPadron.ToString()
            };

            // * Actualizar la localizacion
            if(request.Latitud != 0 && request.Longitud != 0){
                ActualizarLocalizacion(oficina, request.IdPadron, request.Latitud, request.Longitud);
            }

            // * Agregar observaciones
            if( !string.IsNullOrEmpty(request.Comentarios)){
                AgregarObservaciones(oficina, request.IdPadron, request.CodigoCuenta, string.Format("[UBITOMA] - {0}", request.Comentarios));
            }

            _tempResp.Actualizado = 1;
            _tempResp.Mensaje = "Actualizado";
        }


        /// <summary>
        /// Inserta una nueva observacion a la tabla de observaciones
        /// </summary>
        /// <param name="enlace"></param>
        /// <param name="id_padron"></param>
        /// <param name="id_cuenta"></param>
        /// <param name="observacion"></param>
        /// <param name="operador"></param>
        private void AgregarObservaciones(Ruta enlace, int id_padron, int id_cuenta, string observacion, string operador = "CONSULTA")
        {
            var _idOperador = ObtenerIdUsuarioPorUsuario(enlace, operador);
            
            using( var sqlConenction = new SqlConnection(enlace.ConnectionString)){
                sqlConenction.Open();

                //***** Comprobar si ya existe la observacion
                bool existe = false;
                using(var command = new SqlCommand()){
                    var _query = $"Select id_observacion From [Padron].[Opr_Observaciones] Where id_padron = {id_padron} and observacion = '{observacion}' and CAST(DATEADD(DAY, 7, fecha) as date) > CAST( GETDATE() as date) ";
                    command.CommandText = _query;
                    command.Connection = sqlConenction;
                    using(var reader = command.ExecuteReader()){
                        if(reader.Read()){
                            var _tmpId = int.TryParse( reader["id_observacion"].ToString(), out int tmpint)?tmpint:0;
                            if(_tmpId > 0){
                                existe = true;
                            }
                        }
                    }
                }

                if(!existe){
                    // Agregar Observacion
                    using (var command = new SqlCommand()){
                        var _query = " Insert Into Padron.Opr_Observaciones (fecha, id_padron, id_cuenta, id_operador, id_sucursal, observacion, mostrar_hasta) " +
                        $" Values ( GETDATE(), {id_padron}, {id_cuenta}, '{_idOperador}', 1, '{observacion}', DATEADD(MONTH, 1, GETDATE()) ) ";
                        command.CommandText = _query;
                        command.Connection = sqlConenction;
                        command.ExecuteNonQuery();
                    }
                }

                sqlConenction.Close();
            }

        }
        
        /// <summary>
        /// Obtiene el idUsuario de la tabla Sys_Usuario a partert del nombre del usuario
        /// </summary>
        /// <param name="enlace"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        private string ObtenerIdUsuarioPorUsuario(Ruta enlace, string usuario)
        {
            var _result = "NERUS";
            using(var connection = new SqlConnection(enlace.ConnectionString))
            {
                connection.Open();
                var _query = $"SELECT id_usuario, nombre FROM [Global].[Sys_Usuarios] Where usuario = '{usuario}'";
                var _command = new SqlCommand(_query, connection);
                using(var reader = _command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        _result = reader["id_usuario"].ToString()!;
                    }
                }
                connection.Close();
            }
            return _result;
        }

        /// <summary>
        /// Actualiza la localizacion en la tabla cat_padron
        /// </summary>
        /// <param name="oficina"></param>
        /// <param name="latitud"></param>
        /// <param name="longitud"></param>
        private void ActualizarLocalizacion(Ruta oficina, long idPadron, double latitud, double longitud){
            try{
                using(var sqlConnection = new SqlConnection(oficina.ConnectionString)){
                    sqlConnection.Open();
                    string _query = $"Update [padron].[cat_padron] Set latitud={latitud}, longitud={longitud}, mensaje = 'Ubitoma' Where id_padron = {idPadron}";
                    var _command = new SqlCommand(_query, sqlConnection);
                    var res = _command.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch(Exception err){
                logger.LogError(err, $"Error al tratar de actualizar la localizacion del padron ${idPadron} de la oficina {oficina.Oficina}");
            }
        }

        /// <summary>
        /// Regresa un objecto del tipo OprActualizacion que representa los valores actuales del padron
        /// </summary>
        /// <param name="oficina"></param>
        /// <param name="idPadron"></param>
        /// <returns></returns>
        public OprActualizacion? ObtenerValoresActualizacion(Ruta oficina, int idPadron){
            OprActualizacion? result = null;
            using(var context = new ArquosContext(oficina.ConnectionString)){
                var _data = context.VwCatPadrons.Where(item => item.IdPadron == idPadron).FirstOrDefault();
                if(_data != null){
                    result = OprActualizacionBuilder.FromModel(_data);
                    result.IdOficina = oficina.Id;
                }
            }
            return result;
        }

    }
}
