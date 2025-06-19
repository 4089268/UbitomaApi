using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using RutasApi.Models;

namespace RutasApi.Models{
    public class ConexionSQL{

        public static List<CuentaModel> ObtenerCuentas(string cadConexion){
            var _tmpList = new List<CuentaModel>();

            //****** Consulta datos
            var _query = "Select Concat(sb,'-',sector,'-',manzana) as ruta, id_cuenta, razon_social, _estatus, _situacion, total, latitud, longitud, direccion, _colonia, sb, sector, manzana, lote, _localizacion, id_padron, id_medidor " +
                        "From [Padron].[vw_Cat_Padron] " +
                        "Where (IsNull(latitud,0) = 0  Or IsNull(longitud,0) = 0 ) And id_padron > 0 and id_cuenta > 0 " +
                        "order by sb, sector, manzana, razon_social " ;

            using (var _conexion = new SqlConnection(cadConexion)){
                _conexion.Open();
                using(var _command = new SqlCommand(_query, _conexion)){
                    using(SqlDataReader _reader = _command.ExecuteReader()){
                        while(_reader.Read()){
                            _tmpList.Add( new CuentaModel{
                                Ruta            = _reader["ruta"].ToString() ?? "",
                                IdCuenta       = long.Parse(_reader["id_cuenta"].ToString() ?? "0"),
                                RazonSocial    = _reader["razon_social"].ToString() ?? "",
                                Estatus         = _reader["_estatus"].ToString() ?? "",
                                Situacion       = _reader["_situacion"].ToString() ?? "",
                                Latitud         = _reader["latitud"].ToString() ?? "",
                                Longitud        = _reader["longitud"].ToString() ?? "",
                                Direccion       = _reader["direccion"].ToString() ?? "",
                                Colonia         = _reader["_colonia"].ToString() ?? "",
                                Manzana         = int.Parse(_reader["manzana"].ToString() ?? "0"),
                                Lote            = _reader["lote"].ToString() ?? "",
                                Localizacion    = _reader["_localizacion"].ToString() ?? "",
                                Saldo           = decimal.Parse(_reader["total"].ToString() ?? "0"),
                                Subsistema      = int.Parse(_reader["sb"].ToString() ?? "0" ),
                                Sector          = int.Parse(_reader["sector"].ToString() ?? "0"),
                                IdPadron       = long.Parse(_reader["id_padron"].ToString() ?? "0"),
                                Medidor         = _reader["id_medidor"].ToString() ?? "",
                            });
                        }
                    }
                }
                _conexion.Close();
            }
            return _tmpList;

	    }

        public static DatosEmpresa ObtenerDatosEmpresa(string cadConexion){
            var _tmpData = new DatosEmpresa();
            var _query = "Select  nombre_comercial, razon_social, direccion, colonia, ciudad, estado, rfc From [Global].[Cat_Empresa] ";
            using (var _conexion = new SqlConnection(cadConexion)){
                    _conexion.Open();
                    using(var _command = new SqlCommand(_query, _conexion)){
                        using(SqlDataReader _reader = _command.ExecuteReader()){
                            if(_reader.Read()){
                                _tmpData.Nombre_Comercial    = _reader["nombre_comercial"].ToString();
                                _tmpData.Razon_Social        = _reader["razon_social"].ToString();
                                _tmpData.Direccion           = _reader["direccion"].ToString();
                                _tmpData.Colonia             = _reader["colonia"].ToString();
                                _tmpData.Ciudad              = _reader["ciudad"].ToString();
                                _tmpData.Estado              = _reader["estado"].ToString();
                                _tmpData.Rfc                 = _reader["rfc"].ToString();
                            }
                        }
                    }
                    _conexion.Close();
                }
            return _tmpData;
        }

    }
}
