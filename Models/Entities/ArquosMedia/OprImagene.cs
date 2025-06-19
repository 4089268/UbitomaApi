using System;
using System.Collections.Generic;

#nullable disable

namespace RutasApi.Models.Entities.ArquosMedia
{
    public partial class OprImagene
    {
        public decimal IdImagen { get; set; }
        public string IdTabla { get; set; }
        public string Tabla { get; set; }
        public string Descripcion { get; set; }
        public byte[] Imagen { get; set; }
        public string Archivo { get; set; }
        public string IdInsert { get; set; }
        public DateTime? FechaInsert { get; set; }
        public decimal? IdMediatype { get; set; }
        public string Maquina { get; set; }
        public decimal? IdPadron { get; set; }
        public byte[] Documento { get; set; }
        public decimal? Filesize { get; set; }
        public string FileExtension { get; set; }
    }
}
