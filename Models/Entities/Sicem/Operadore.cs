using System;
using System.Collections.Generic;

namespace RutasApi.Models.Entities.Sicem
{
    public partial class Operadore
    {
        public Operadore()
        {
            SesionOperadores = new HashSet<SesionOperadore>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Usuario { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? RutaId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Ruta? Ruta { get; set; }
        
        public virtual ICollection<SesionOperadore> SesionOperadores { get; set; } = [];
        public virtual ICollection<OprActualizacion> OprActualizacions { get; set; } = [];
    }
}
