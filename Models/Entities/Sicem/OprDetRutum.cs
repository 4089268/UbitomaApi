using System;
using System.Collections.Generic;

namespace RutasApi.Models.Entities.Sicem
{
    public partial class OprDetRutum
    {
        public int Id { get; set; }
        public int OprRutaId { get; set; }
        public int IdPadron { get; set; }
        public int Cuenta { get; set; }
        public string? RazonSocial { get; set; }

        public virtual OprRutum OprRuta { get; set; } = null!;
    }
}
