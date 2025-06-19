using System;
using System.Collections.Generic;

namespace RutasApi.Models.Request{
    public class RutasActualizarRequest{
        public RutasActualizarItemRequest[] Cuentas {get;set;} = default!;
    }

    public class RutasActualizarItemRequest{
        public string Id_Padron{get;set;} = default!;
	    public string Id_Cuenta {get;set;} = "0";
        public string Latitud {get;set;} = "";
        public string Longitud {get;set;} = "";
        public string Observaciones {get;set;} = "";
        public int Id_Estatus {get;set;} = 0;
        public int Id_Situacion {get;set;} = 0;
        public int Id_Tarifa {get;set;} = 0;
        public int Id_Giro {get;set;} = 0;
        public int Id_AnomaliaPredio {get;set;} = 0;
    }
}
