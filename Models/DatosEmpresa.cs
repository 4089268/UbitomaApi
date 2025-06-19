using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RutasApi.Models{
    public class DatosEmpresa{
        public string? Nombre_Comercial {get;set;}
        public string? Razon_Social {get;set;}
        public string? Direccion {get;set;}
        public string? Colonia {get;set;}
        public string? Ciudad {get;set;}
        public string? Estado {get;set;}
        public string? Rfc {get;set;}
    }
}