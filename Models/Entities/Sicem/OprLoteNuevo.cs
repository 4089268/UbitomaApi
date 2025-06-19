using System;
using System.Collections.Generic;

namespace RutasApi.Models.Entities.Sicem
{
    public partial class OprLoteNuevo
    {
        public int Id { get; set; }
        public int IdOficina { get; set; }
        public int IdPadron { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Observaciones { get; set; }
        public int? IdGiro { get; set; }
        public string? Giro { get; set; }
        public int? IdAnomaliaPredio { get; set; }
        public string? AnomaliaPredio { get; set; }
        public string? Calle { get; set; }
        public string? NumeroExt { get; set; }
        public string? NumeroInt { get; set; }
        public string? CalleLat1 { get; set; }
        public string? CalleLat2 { get; set; }
        public string? Colonia { get; set; }
        public string? TipoToma { get; set; }
        public string? TipoInstalacion { get; set; }
        public int? OperadorId { get; set; }

        public virtual Ruta IdOficinaNavigation { get; set; } = null!;
        public virtual Operadore? Operador { get; set; }
    }
}
