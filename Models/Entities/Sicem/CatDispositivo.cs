using System;
using System.Collections.Generic;

namespace RutasApi.Models.Entities.Sicem
{
    public partial class CatDispositivo
    {
        public int Id { get; set; }
        public decimal? IdUsuario { get; set; }
        public string? Imei { get; set; }
        public string? Descripcion { get; set; }
        public int? Oficina { get; set; }
        public DateTime? Caduca { get; set; }
        public bool? Inactivo { get; set; }
    }
}
