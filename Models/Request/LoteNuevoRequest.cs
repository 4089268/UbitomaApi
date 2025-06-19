using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using RutasApi.Models.Entities.Sicem;

namespace RutasApi.Models.Request
{
    public class LoteNuevoRequest
    {
        [Required]
        public int IdPadron { get; set; }
        [Required]
        public double Latitud { get; set; }
        [Required]
        public double Longitud { get; set; }
        public string? Fecha { get; set; }
        public string? Comentarios { get; set; }
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

        public OprLoteNuevo ToEntity(int idOficina, int idOperador)
        {
            var newItem = new OprLoteNuevo()
            {
                IdOficina = idOficina,
                OperadorId = idOperador,
                IdPadron = this.IdPadron,
                Latitud = this.Latitud.ToString(),
                Longitud = this.Longitud.ToString(),
                Fecha = DateTime.TryParseExact( this.Fecha, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime fechaParsed) ? fechaParsed : DateTime.Now,
                Observaciones = this.Comentarios,
                IdGiro = this.IdGiro,
                Giro = this.Giro,
                IdAnomaliaPredio = this.IdAnomaliaPredio,
                AnomaliaPredio = this.AnomaliaPredio,
                Calle = this.Calle,
                NumeroExt = this.NumeroExt,
                NumeroInt = this.NumeroInt,
                CalleLat1 = this.CalleLat1,
                CalleLat2 = this.CalleLat2,
                Colonia = this.Colonia,
                TipoToma = this.TipoToma,
                TipoInstalacion = this.TipoInstalacion
            };
            return newItem;
        }
    }

}