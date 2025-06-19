using System;

namespace RutasApi.Models.Responses{
    public class RutasTotalResponse{
        public string Ruta {get;set;} = default!;
        public int Total {get;set;}
    }

}