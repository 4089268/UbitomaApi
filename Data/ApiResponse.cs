using System;

namespace RutasApi.Data{

    public class ApiResponse<T>{
        public ApiResponse() {
            this.Ok = 0;
            this.Mensaje = "";
        }

        public int Ok {get;set;}
        public string Mensaje {get;set;}
        public T Datos {get;set;} = default!;

    }

}