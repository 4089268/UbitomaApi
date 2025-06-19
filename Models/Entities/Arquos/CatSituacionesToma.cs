using System;
using System.Collections.Generic;

namespace RutasApi.Models.Entities.Arquos
{
    public partial class CatSituacionesToma
    {
        public decimal IdSituacion { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool Inactivo { get; set; }
        public decimal IdTrabajo { get; set; }
        public decimal? RegsPadron { get; set; }
        public DateTime? RegsCounted { get; set; }
    }
}
