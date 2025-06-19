using System;
using RutasApi.Models.Entities.Sicem;

namespace RutasApi.Models {
    public class HistActualizacion {
        public int IdOficina { get{ return ValorNuevo.IdOficina; } }
        public string Oficina { get{ return ValorNuevo.Oficina; } }
        public int IdCuenta { get{ return ValorNuevo.IdCuenta; } }
        public int IdPadron { get{ return ValorNuevo.IdPadron; } }

        public ActualizacionItem ValorNuevo {get; set;} = default!;
        public ActualizacionItem ValorAnterior {get; set;} = default!;
    }
    
    public class ActualizacionItem {
        public int Id { get; set; }
        public int IdOficina { get; set; }
        public string Oficina {get;set;}
        public int IdPadron { get; set; }
        public int IdCuenta { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Observaciones { get; set; }
        public int IdEstatus { get; set; }
        public string Estatus { get; set; }
        public int IdSituacion { get; set; }
        public string Situacion { get; set; }
        public int IdTarifa { get; set; }
        public string Tarifa { get; set; }
        public int IdGiro { get; set; }
        public string Giro { get; set; }
        public int IdAnomaliaPredio { get; set; }
        public string AnomaliaPredio { get; set; }
        public DateTime? Fecha { get; set; }
        
        public ActualizacionItem(){
            Id = 0;
            IdOficina = 0;
            Oficina = "";
            IdPadron = 0;
            IdCuenta = 0;
            Latitud = "";
            Longitud = "";
            Observaciones = "";
            IdEstatus = 0;
            Estatus = "";
            IdSituacion = 0;
            Situacion = "";
            IdTarifa = 0;
            Tarifa = "";
            IdGiro = 0;
            Giro = "";
            IdAnomaliaPredio = 0;
            AnomaliaPredio = "";
        }

        public static ActualizacionItem FromOprActualizacion(OprActualizacion data){
            var item = new ActualizacionItem
            {
                Id = data.Id,
                IdOficina = data.IdOficina,
                IdPadron = data.IdPadron,
                IdCuenta = data.IdCuenta,
                Latitud = data.Latitud ?? "",
                Longitud = data.Longitud ?? "",
                Observaciones = data.Observaciones ?? "",
                IdEstatus = data.IdEstatus ?? 0,
                Estatus = data.Estatus ?? "",
                IdSituacion = data.IdSituacion ?? 0,
                Situacion = data.Situacion ?? "",
                IdTarifa = data.IdTarifa ?? 0,
                Tarifa = data.Tarifa ?? "",
                IdGiro = data.IdGiro ?? 0,
                Giro = data.Giro ?? "",
                Fecha = data.Fecha,
                IdAnomaliaPredio = data.IdAnomaliaPredio ?? 0,
                AnomaliaPredio = data.AnomaliaPredio ?? ""
            };
            return item;
        }

    }
}