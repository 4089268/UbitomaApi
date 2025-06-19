using System;
using System.Collections.Generic;
using RutasApi.Models.Request;
using RutasApi.Models.Entities.Arquos;

namespace RutasApi.Models.Entities.Sicem {
    public class OprActualizacionBuilder {
        public static OprActualizacion FromModel(VwCatPadron pad){
            var result = new OprActualizacion();
            result = new OprActualizacion
            {
                Id = 0,
                IdPadron = (int)pad.IdPadron,
                IdCuenta = (int)pad.IdCuenta,
                Latitud = pad.Latitud?.ToString(),
                Longitud = pad.Longitud?.ToString(),
                Observaciones = "",
                IdEstatus = (int?)pad.IdEstatus,
                Estatus = pad.Estatus,
                IdSituacion = (int?)pad.IdSituacion,
                Situacion = pad.Situacion,
                IdTarifa = (int?)pad.IdTarifa,
                Tarifa = pad.Tipousuario,
                IdGiro = (int?)pad.IdGiro,
                Giro = pad.Giro,
                IdAnomaliaPredio = 0,
                AnomaliaPredio = "",
                Fecha = DateTime.Now
            };
            return result;
        }

        public static OprActualizacion FromModel(RutasActualizarItemRequest req){
            var result = new OprActualizacion();
            result = new OprActualizacion
            {
                Id = 0,
                IdPadron = Convert.ToInt32(req.Id_Padron),
                IdCuenta = Convert.ToInt32(req.Id_Cuenta),
                Latitud = req.Latitud,
                Longitud =req.Longitud,
                Observaciones = req.Observaciones,
                IdEstatus = req.Id_Estatus,
                Estatus = "",
                IdSituacion = req.Id_Situacion,
                Situacion = "",
                IdTarifa = req.Id_Tarifa,
                Tarifa = "",
                IdGiro = req.Id_Giro,
                Giro = "",
                IdAnomaliaPredio = req.Id_AnomaliaPredio,
                AnomaliaPredio = "",
                Fecha = DateTime.Now
            };
            return result;
        }

        public static OprActualizacion FromModel(CapturaRequest req){
            var result = new OprActualizacion();
            result = new OprActualizacion
            {
                IdPadron = Convert.ToInt32(req.IdPadron),
                IdCuenta = Convert.ToInt32(req.CodigoCuenta),
                Latitud = req.Latitud.ToString(),
                Longitud = req.Longitud.ToString(),
                Observaciones = req.Comentarios,
                IdEstatus = req.IdEstatus,
                Estatus = req.Estatus,
                IdSituacion = req.IdSituacion,
                Situacion = req.Situacion,
                IdTarifa = req.IdTarifa,
                Tarifa = req.Tarifa,
                IdGiro = req.IdGiro,
                Giro = req.Giro,
                IdAnomaliaPredio = req.IdAnomaliaPredio,
                AnomaliaPredio = req.AnomaliaPredio,
                RazonSocial = req.RazonSocial,
                Calle = req.Calle,
                NumeroExt = req.NumeroExt,
                NumeroInt = req.NumeroInt,
                CalleLat1 = req.CalleLat1,
                CalleLat2 = req.CalleLat2,
                Colonia = req.Colonia,
                TienePozo = req.TienePozo == 1,
                DiametroToma = req.DiametroToma,
                NumeroMedidor = req.NumeroMedidor,
                TipoToma = req.TipoToma,
                TipoInstalacion = req.TipoInstalacion,
                MarcaMedidor = req.MarcaMedidor,
                SectorHidraulico = req.SectorHidraulico,
                Fecha = DateTime.Now
            };
            return result;
        }

    }
}