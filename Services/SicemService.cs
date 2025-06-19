using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RutasApi.Data;
using RutasApi.Interfaces;
using RutasApi.Models.Entities.Sicem;
using RutasApi.Models.Request;

namespace RutasApi.Services {
    public class SicemService{
        private readonly SicemContext sicemContext;
        private readonly ArquosService arquosService;
        private readonly ICatalogService catalogService;
        private readonly ILogger<SicemService> logger;

        public SicemService(SicemContext c, ArquosService arquosService, ICatalogService catalogService, ILogger<SicemService> logger){
            this.sicemContext = c;
            this.arquosService = arquosService;
            this.catalogService = catalogService;
            this.logger = logger;
        }

        public ICollection<object>? ObtenerCatalogoOficinas(){
            try{
                var oficinas = sicemContext.Rutas.Where(item => item.Inactivo == false ).Select( item => new{Id=item.Id, Oficina=item.Oficina}).ToArray<object>();
                return oficinas;
            }catch(Exception err){
                Console.WriteLine($">> Error al obtener el catalogo de oficinas Error:{err.Message}\n\tStacktrace:{err.StackTrace}");
                return null;
            }
        }

        public Ruta? ObtenerRuta(int idOficina){
            try
            {
                var oficina = sicemContext.Rutas.Where(item => item.Id == idOficina && item.Inactivo == false).FirstOrDefault();
                return oficina;
            }catch(Exception err){
                Console.WriteLine($">>Error al obtener la oficina Err:{err.Message}\n\tStacktrace:{err.StackTrace}");
                return null;
            }
        }

        public ICollection<Ruta>? ObtenerRutas(){
            try{
                var oficina = sicemContext.Rutas.Where(item => item.Inactivo == false).ToList();
                return oficina;
            }catch(Exception err){
                Console.WriteLine($">>Error el listado de oficinas Err:{err.Message}\n\tStacktrace:{err.StackTrace}");
                return null;
            }
        }
        
        /// <summary>
        /// Inserta el registro de actualizacion en la tabla Ubitoma.Opr_Actualizacion y regresa el id generado
        /// </summary>
        /// <param name="oficina"></param>
        /// <param name="request"></param>
        public int RegistrarHistorialActualizacion(Ruta oficina, RutasActualizarItemRequest request){
            
            // TODO: move this methdos to another class
            // TODO: move this methdos to another class

            // Obtener los datos del padron actual
            OprActualizacion _registroDatosAnteriores = arquosService.ObtenerValoresActualizacion(oficina, Convert.ToInt32(request.Id_Padron))!;
            
            // Crear el objeto de registro con los valores nuevos
            var _registro = OprActualizacionBuilder.FromModel(request);
            _registro.IdOficina = oficina.Id;
            _registro.Fecha = DateTime.Now;

            // Actualizar los valores de los catalogos
            if(_registro.IdEstatus > 0){
                var _cat = this.catalogService.Estatus(oficina)!.Where(map => ((int)map["id"]) == _registro.IdEstatus).FirstOrDefault();
                if(_cat != null){
                    _registro.Estatus = _cat["descripcion"].ToString();
                }
            }
            if(_registro.IdTarifa > 0){
                var _cat = this.catalogService.Tarifas(oficina)!.Where(map => ((int)map["id"]) == _registro.IdTarifa).FirstOrDefault();
                if(_cat != null){
                    _registro.Tarifa = _cat["descripcion"].ToString();
                }
            }
            if(_registro.IdSituacion > 0){
                var _cat = this.catalogService.SituacionesPredio(oficina)!.Where(map => ((int)map["id"]) == _registro.IdSituacion).FirstOrDefault();
                if(_cat != null){
                    _registro.Situacion = _cat["descripcion"].ToString();
                }
            }
            if(_registro.IdGiro > 0){
                var _cat = this.catalogService.Giros(oficina)!.Where(map => ((int)map["id"]) == _registro.IdGiro).FirstOrDefault();
                if(_cat != null){
                    _registro.Giro = _cat["descripcion"].ToString();
                }
            }
            if(_registro.IdAnomaliaPredio > 0 ){
                var _cat = this.catalogService.AnomaliasPredio(oficina)!.Where(map => ((int)map["id"]) == _registro.IdAnomaliaPredio).FirstOrDefault();
                if(_cat != null){
                    _registro.AnomaliaPredio = _cat["descripcion"].ToString();
                }
            }


            if(_registroDatosAnteriores != null){

                // Almacenar el registro con datos anteriores y obtner el id
                _registroDatosAnteriores.Fecha = DateTime.Now;
                sicemContext.OprActualizacions.Add(_registroDatosAnteriores);
                sicemContext.SaveChanges();

                // Asignarle la referencia al registro de datos anteriores
                _registro.IdActualizacionAnt = _registroDatosAnteriores.Id;

                // Comprar los datos del registro con los datos anteriores
                _registro.IdCuenta = _registroDatosAnteriores.IdCuenta;
                if( Convert.ToInt32(_registro.Latitud??"0") == 0 || Convert.ToInt32(_registro.Longitud??"0") == 0 ){

                    _registro.Latitud = _registroDatosAnteriores.Latitud;
                    _registro.Longitud = _registroDatosAnteriores.Longitud;
                }
                if(_registro.IdEstatus <= 0){
                    _registro.IdEstatus = _registroDatosAnteriores.IdEstatus;
                    _registro.Estatus = _registroDatosAnteriores.Estatus;
                }
                if(_registro.IdTarifa <= 0){
                    _registro.IdTarifa = _registroDatosAnteriores.IdTarifa;
                    _registro.Tarifa = _registroDatosAnteriores.Tarifa;
                }
                if(_registro.IdSituacion <= 0){
                    _registro.IdSituacion = _registroDatosAnteriores.IdSituacion;
                    _registro.Situacion = _registroDatosAnteriores.Situacion;
                }
                if(_registro.IdGiro <= 0){
                    _registro.IdGiro = _registroDatosAnteriores.IdGiro;
                    _registro.Giro = _registroDatosAnteriores.Giro;
                }
                if(_registro.IdAnomaliaPredio <= 0){
                    _registro.IdGiro = _registroDatosAnteriores.IdAnomaliaPredio;
                    _registro.Giro = _registroDatosAnteriores.AnomaliaPredio;
                }
                
            }

            // Insertar el registro
            sicemContext.OprActualizacions.Add(_registro);
            sicemContext.SaveChanges();

            return _registro.Id;
        }
        
        public int RegistrarHistorialActualizacion(Ruta oficina, CapturaRequest request, int operadorID = 0){
            
            // * convert CapturaRequest into OprActualizacion
            OprActualizacion newActualizacionRecord = OprActualizacionBuilder.FromModel(request);
            newActualizacionRecord.IdOficina = oficina.Id;
            newActualizacionRecord.IdOperador = operadorID;

            // * get last modification of the current padron
            var _lastActualizacion = this.sicemContext.OprActualizacions
                .Where( item => item.IdOficina == newActualizacionRecord.IdOficina)
                .Where( item => item.IdPadron == newActualizacionRecord.IdPadron)
                .OrderByDescending( item => item.Fecha)
                .FirstOrDefault();

            if( _lastActualizacion != null){
                newActualizacionRecord.IdActualizacionAnt = _lastActualizacion.Id;
            }

            // * attempt to store the request
            try {
                this.sicemContext.OprActualizacions.Add(newActualizacionRecord);
                this.sicemContext.SaveChanges();
                logger.LogInformation("New record of OprActualizacion with id:'{recordId}'", newActualizacionRecord.Id);
                return newActualizacionRecord.Id;
            }
            catch (System.Exception ex) {
                logger.LogError(ex, "Fail to store the actualizacion-record");
                throw;
            }

        }

    }
}