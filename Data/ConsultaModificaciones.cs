using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RutasApi.Models;
using RutasApi.Models.Entities.Sicem;
using RutasApi.Models.Request;
using RutasApi.Services;

namespace RutasApi.Data{
    public class ConsultaModificaciones{
        private ConsultaRequest filtro;
        private SicemContext sicemContext;
        private SicemService sicemService;
        
        public ConsultaModificaciones(ConsultaRequest f, SicemContext c, SicemService s){
            this.filtro = f;
            this.sicemContext = c;
            this.sicemService = s;
        }

        public List<HistActualizacion> RealizarConsulta(){
            var _result = new List<HistActualizacion>();
            
            // Generar consulta
            var _data = sicemContext.OprActualizacions
                .Where(item => item.IdActualizacionAnt != null)
                .Where(item => item.Fecha >= filtro.FechaDesdeP && item.Fecha <= filtro.FechaHastaP)
                .AsQueryable();

            if(filtro.Oficinas.Length > 0){
                _data = _data.Where( item => filtro.Oficinas.Contains(item.IdOficina));
            }
            if(filtro.Padron.Length > 0){
                _data = _data.Where( item => filtro.Padron.Contains(item.IdPadron));
            }
            if(filtro.Cuentas.Length > 0){
                _data = _data.Where(item => filtro.Cuentas.Contains(item.IdCuenta));
            }
            if(filtro.Estatus.Length > 0){
                _data = _data.Where(item => filtro.Estatus.Contains(item.IdEstatus??0));
            }
            if(filtro.Situaciones.Length > 0){
                _data = _data.Where(item => filtro.Situaciones.Contains(item.IdSituacion??0));
            }
            if(filtro.Tarifas.Length > 0){
                _data = _data.Where(item => filtro.Tarifas.Contains(item.IdTarifa??0));
            }
            if(filtro.Giros.Length > 0){
                _data = _data.Where(item => filtro.Giros.Contains(item.IdGiro??0));
            }
            if(filtro.Geolocalizado > 0){
                if( filtro.Geolocalizado == 1){
                    // Obtener cuentas con geolocalizacion
                    _data = _data.Where(item => Convert.ToInt32(item.Latitud??"0") != 0);
                }else{
                    // Obtener cuentas sin geolocalizacion
                    _data = _data.Where(item => Convert.ToInt32(item.Latitud??"0") == 0);
                }
            }
            if(filtro.Observaciones > 0){
                if( filtro.Observaciones == 1){
                    // Obtener cuentas con observaciones
                    _data = _data.Where(item => item.Observaciones!.Length > 0);
                }else{
                    // Obtener cuentas sin observaciones
                    _data = _data.Where(item => item.Observaciones!.Length <= 0);
                }
            }
            //_data = _data.Where(item => (item.Fecha??new DateTime()) >= filtro.FechaDesdeP && (item.Fecha??new DateTime()) <= filtro.FechaHastaP );
            
            // var _registrosNuevos = _data.ToList().Select( item => ActualizacionItem.FromOprActualizacion(item)).ToList();
            var _registrosNuevos = _data.ToList();
            
            
            _result = _registrosNuevos.Select( registro => {
                var itemHist = new HistActualizacion();
                itemHist.ValorNuevo = ActualizacionItem.FromOprActualizacion(registro);

                var oldData = sicemContext.OprActualizacions.Where(item => item.Id == (registro.IdActualizacionAnt??0) ).FirstOrDefault();
                if(oldData != null){
                    itemHist.ValorAnterior = ActualizacionItem.FromOprActualizacion(oldData);
                }
                return itemHist;
            }).ToList();

            // Actualiar valors oficina
            var rutas = sicemService.ObtenerRutas();
            _result.ForEach( hist => {
                var oficina = rutas?.Where(item => item.Id == hist.ValorNuevo.IdOficina ).FirstOrDefault(new Ruta{Oficina="Error"}).Oficina!;
                hist.ValorAnterior.Oficina = oficina;
                hist.ValorNuevo.Oficina = oficina;
            });

            return _result;
        }

    }
}
