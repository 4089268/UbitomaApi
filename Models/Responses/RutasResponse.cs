using System;
using System.Collections.Generic;
using RutasApi.Models;

namespace RutasApi.Models.Responses{

    public class RutasGroupResponse{
        public string Ruta {get;set;} = default!;
        public CuentaModel[] Cuentas {get;set;} = default!;

    }

}