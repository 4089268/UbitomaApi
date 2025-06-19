using System;
using System.Collections.Generic;

namespace RutasApi.Models.Entities.Sicem
{
    public partial class SesionOperadore
    {
        public Guid Id { get; set; }
        public int OperadorId { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime ValidTo { get; set; }

        public virtual Operadore Operador { get; set; } = null!;
    }
}
