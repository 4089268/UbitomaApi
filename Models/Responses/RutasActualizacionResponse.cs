using System;
using System.Collections.Generic;

namespace RutasApi.Models.Responses{
    public class RutasActualizacionResponse {
        public string Id_Cuenta {get;set;} = "";
	    public string Id_Padron {get;set;} = "";
        public int Actualizado {get;set;}
        public string? Mensaje {get;set;}
        
    }
}
