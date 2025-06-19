﻿using System;
using System.Collections.Generic;

namespace RutasApi.Models.Entities.Arquos
{
    public partial class VwCatPadron
    {
        public decimal IdPadron { get; set; }
        public decimal IdCuenta { get; set; }
        public string? NomComercial { get; set; }
        public string? NomPropietario { get; set; }
        public string? RazonSocial { get; set; }
        public string? Rfc { get; set; }
        public string? Curp { get; set; }
        public string? Direccion { get; set; }
        public string? Colonia { get; set; }
        public string? Ciudad { get; set; }
        public string? Estado { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Telefono1 { get; set; }
        public string? Telefono2 { get; set; }
        public string? Telefono3 { get; set; }
        public bool? ReciboMail { get; set; }
        public string? Email { get; set; }
        public string? PaginaInternet { get; set; }
        public string? CallePpal { get; set; }
        public string? NumExt { get; set; }
        public string? NumInt { get; set; }
        public string? CalleLat1 { get; set; }
        public string? CalleLat2 { get; set; }
        public decimal? Ruta { get; set; }
        public decimal? Sb { get; set; }
        public decimal? Sector { get; set; }
        public decimal? Manzana { get; set; }
        public decimal? Lote { get; set; }
        public decimal? Nivel { get; set; }
        public decimal? Fraccion { get; set; }
        public decimal? Toma { get; set; }
        public string? Localizacion { get; set; }
        public decimal? IdLocalidad { get; set; }
        public decimal IdColonia { get; set; }
        public decimal? IdGiro { get; set; }
        public decimal? AreaLote { get; set; }
        public decimal? AreaConstruida { get; set; }
        public decimal? AreaJardin { get; set; }
        public decimal? IdClaseusuario { get; set; }
        public decimal? Viviendas { get; set; }
        public decimal? SalidasHidraulicas { get; set; }
        public decimal? Frente { get; set; }
        public decimal? IdEstatus { get; set; }
        public decimal? LpsPagados { get; set; }
        public decimal? Mf { get; set; }
        public decimal? Af { get; set; }
        public decimal? Desviacion { get; set; }
        public decimal? PromedioAnt { get; set; }
        public decimal? PromedioAct { get; set; }
        public decimal? MesAdeudoAnt { get; set; }
        public decimal? MesAdeudoAct { get; set; }
        public decimal? IdServicio { get; set; }
        public decimal? IdTarifa { get; set; }
        public decimal? IdTarifafija { get; set; }
        public decimal? ConsumoFijo { get; set; }
        public decimal? ImporteFijo { get; set; }
        public decimal? ImporteFijoDren { get; set; }
        public decimal? ImporteFijoSane { get; set; }
        public decimal? IdSituacion { get; set; }
        public decimal? IdAnomaliaAct { get; set; }
        public decimal? LecturaAnt { get; set; }
        public decimal? LecturaAct { get; set; }
        public decimal? ConsumoAnt { get; set; }
        public decimal? ConsumoAct { get; set; }
        public decimal? ConsumoRealAnt { get; set; }
        public decimal? ConsumoRealAct { get; set; }
        public decimal? IdTipocalculo { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Iva { get; set; }
        public decimal? Total { get; set; }
        public bool? Prefacturado { get; set; }
        public bool? Anual { get; set; }
        public decimal? Recibo { get; set; }
        public decimal? ConsumoForzado { get; set; }
        public bool? EsFiscal { get; set; }
        public decimal? PorDescto { get; set; }
        public bool? Visitar { get; set; }
        public decimal? Longitud { get; set; }
        public decimal? Latitud { get; set; }
        public string? LastIdAbono { get; set; }
        public string? LastIdVenta { get; set; }
        public bool? EsMacromedidor { get; set; }
        public bool? TienePozo { get; set; }
        public bool? EsDraef { get; set; }
        public decimal? IdTipodraef { get; set; }
        public string? Tipodraef { get; set; }
        public string? IdMedidor { get; set; }
        public bool? EsAltoconsumidor { get; set; }
        public string Colonia1 { get; set; } = null!;
        public string Poblacion { get; set; } = null!;
        public string Estatus { get; set; } = null!;
        public string Calculo { get; set; } = null!;
        public string Servicio { get; set; } = null!;
        public string Tipousuario { get; set; } = null!;
        public string Nivelsocial { get; set; } = null!;
        public string? Tarifafija { get; set; }
        public string Claseusuario { get; set; } = null!;
        public string Tipogrupo { get; set; } = null!;
        public string? Giro { get; set; }
        public string Situacion { get; set; } = null!;
        public string? AnomaliaAct { get; set; }
        public string? AnomaliaAnt { get; set; }
        public string Diametro { get; set; } = null!;
        public string Hidrocircuito { get; set; } = null!;
        public string? CallePpal1 { get; set; }
        public string? CalleLat11 { get; set; }
        public string? CalleLat21 { get; set; }
        public string Materialmedidor { get; set; } = null!;
        public string Tipoinstalacion { get; set; } = null!;
        public string Ubicacionmedidor { get; set; } = null!;
        public string CalculoAct { get; set; } = null!;
        public string CalculoAnt { get; set; } = null!;
        public string Zona { get; set; } = null!;
        public string Tipotoma { get; set; } = null!;
        public string MaterialToma { get; set; } = null!;
        public string MaterialCalle { get; set; } = null!;
        public string MaterialBanqueta { get; set; } = null!;
        public string Tipotuberia { get; set; } = null!;
        public string Tipofactible { get; set; } = null!;
        public string? MesFacturado { get; set; }
        public string? FechaAlta { get; set; }
        public string? AltaFactura { get; set; }
        public string? FechaLecturaAnt { get; set; }
        public string? FechaLecturaAct { get; set; }
        public string? FechaFacturaAnt { get; set; }
        public string? FechaFacturaAct { get; set; }
        public string? FechaVencimiento { get; set; }
        public string? FechaVencimientoAct { get; set; }
        public string? FechaInsert { get; set; }
    }
}
