using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RutasApi.Models.Request {
    public class ImagenesPadronRequest {
        public int Id_Padron{ get; set; }
        public string Descripcion { get; set; } = default!;
        public IFormFile Imagen { get; set; } = default!;
    }
}
