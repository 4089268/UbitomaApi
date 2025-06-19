using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RutasApi.Models.Request
{
    
    public class CapturaRequest {
        [Required]
        public int CodigoCuenta { get; set; }
        [Required]
        public int IdPadron { get; set; }
        [Required]
        public double Latitud { get; set; }
        [Required]
        public double Longitud { get; set; }
        public string? Fecha { get; set; }
        public string? Comentarios { get; set; }
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
        public int? TienePozo { get; set; }
        public string? DiametroToma { get; set; }
        public string? NumeroMedidor { get; set; }
        public string? TipoToma { get; set; }
        public string? TipoInstalacion { get; set; }
        public string? MarcaMedidor { get; set; }
        public string? SectorHidraulico { get; set; }
    }

}