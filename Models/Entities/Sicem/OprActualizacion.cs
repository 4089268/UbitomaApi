using System;
using System.Collections.Generic;

namespace RutasApi.Models.Entities.Sicem
{
    public partial class OprActualizacion
    {
        public int Id { get; set; }
        public int IdOficina { get; set; }
        public int IdCuenta { get; set; }
        public int IdPadron { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Observaciones { get; set; }
        public int? IdEstatus { get; set; }
        public string? Estatus { get; set; }
        public int? IdSituacion { get; set; }
        public string? Situacion { get; set; }
        public int? IdTarifa { get; set; }
        public string? Tarifa { get; set; }
        public int? IdGiro { get; set; }
        public string? Giro { get; set; }
        public int? IdAnomaliaPredio { get; set; }
        public string? AnomaliaPredio { get; set; }
        public string? RazonSocial { get; set; }
        public string? Calle { get; set; }
        public string? NumeroExt { get; set; }
        public string? NumeroInt { get; set; }
        public string? CalleLat1 { get; set; }
        public string? CalleLat2 { get; set; }
        public string? Colonia { get; set; }
        public bool? TienePozo { get; set; }
        public string? DiametroToma { get; set; }
        public string? NumeroMedidor { get; set; }
        public string? TipoToma { get; set; }
        public string? TipoInstalacion { get; set; }
        public string? MarcaMedidor { get; set; }
        public string? SectorHidraulico { get; set; }
        public int? IdActualizacionAnt { get; set; }
        public int? IdOperador { get; set; }

        public virtual Ruta IdOficinaNavigation { get; set; } = null!;
        public virtual Operadore IOperadoreNavigation { get; set; } = null!;
    }
}
