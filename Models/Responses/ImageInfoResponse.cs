using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RutasApi.Models.Responses
{
    public class ImageInfoResponse
    {
        
        public int Id_Imagen { get; set; }
        public string? Tabla { get; set; }
        public string? Descripcion { get; set; }
        public string? Archivo { get; set; }
        public string? FechaInsert { get; set; }
        public int Id_Padron { get; set; }
        public int Filesize { get; set; }
        public string? FileExtension { get; set; }
    }
}