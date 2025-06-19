using System;
using System.Collections.Generic;

namespace RutasApi.Models.Entities.Arquos
{
    public partial class CatEstatus
    {
        public decimal IdEstatus { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Tabla { get; set; } = null!;
        public decimal? RegsPadron { get; set; }
        public DateTime? RegsCounted { get; set; }
    }
}
