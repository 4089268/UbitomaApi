﻿using System;
using System.Collections.Generic;

namespace RutasApi.Models.Entities.Arquos
{
    public partial class CatTiposUsuario
    {
        public decimal IdTipousuario { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool Inactivo { get; set; }
        public decimal? TasaIva { get; set; }
        public decimal? FactorDrenaje { get; set; }
        public decimal? FactorTratamto { get; set; }
        public decimal? Tipo { get; set; }
        public decimal? ImpAguaFijo { get; set; }
        public decimal? ImpDrenajeFijo { get; set; }
        public decimal? ImpSaneamientoFijo { get; set; }
        public decimal? MetrosBase { get; set; }
        public decimal? VecesPromAlto { get; set; }
        public decimal? VecesPromMenor { get; set; }
        public decimal? RegsPadron { get; set; }
        public DateTime? RegsCounted { get; set; }
        public decimal? CostoM3descarga { get; set; }
    }
}
