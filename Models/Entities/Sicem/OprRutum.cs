using System;
using System.Collections.Generic;

namespace RutasApi.Models.Entities.Sicem
{
    public partial class OprRutum
    {
        public OprRutum()
        {
            OprDetRuta = new HashSet<OprDetRutum>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int RutaId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Ruta Ruta { get; set; } = null!;
        public virtual ICollection<OprDetRutum> OprDetRuta { get; set; }
    }
}
