using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RutasApi.Models.Entities.Arquos;

namespace RutasApi.Data
{
    public partial class ArquosContext : DbContext
    {
        private readonly string cadConexion;
        public ArquosContext(string c) {
            this.cadConexion = c;
        }

        public ArquosContext(DbContextOptions<ArquosContext> options)
            : base(options)
        {
            cadConexion = string.Empty;
        }

        public virtual DbSet<CatEstatus> CatEstatuses { get; set; } = null!;
        public virtual DbSet<CatGiro> CatGiros { get; set; } = null!;
        public virtual DbSet<CatSituacionesToma> CatSituacionesTomas { get; set; } = null!;
        public virtual DbSet<CatTiposUsuario> CatTiposUsuarios { get; set; } = null!;
        public virtual DbSet<VwCatPadron> VwCatPadrons { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(cadConexion);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatEstatus>(entity =>
            {
                entity.HasKey(e => e.IdEstatus);

                entity.ToTable("Cat_Estatus", "Global");

                entity.HasIndex(e => new { e.Descripcion, e.Tabla }, "IX_Cat_ST_Desc_Tab")
                    .IsUnique();

                entity.Property(e => e.IdEstatus)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_estatus");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.RegsCounted)
                    .HasColumnType("datetime")
                    .HasColumnName("regsCounted")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RegsPadron)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("regsPadron")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Tabla)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tabla");
            });

            modelBuilder.Entity<CatGiro>(entity =>
            {
                entity.HasKey(e => e.IdGiro);

                entity.ToTable("Cat_Giros", "Padron");

                entity.HasIndex(e => e.Descripcion, "IX_Cat_Giros_Desc")
                    .IsUnique();

                entity.Property(e => e.IdGiro)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_giro");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Inactivo).HasColumnName("inactivo");

                entity.Property(e => e.RegsCounted)
                    .HasColumnType("datetime")
                    .HasColumnName("regsCounted")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RegsPadron)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("regsPadron")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<CatSituacionesToma>(entity =>
            {
                entity.HasKey(e => e.IdSituacion)
                    .HasName("PK_Cat_Situaciontoma");

                entity.ToTable("Cat_SituacionesToma", "Facturacion");

                entity.HasIndex(e => e.Descripcion, "IX_Cat_SituacionesToma_desc")
                    .IsUnique();

                entity.Property(e => e.IdSituacion)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_Situacion");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdTrabajo)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("id_trabajo");

                entity.Property(e => e.Inactivo).HasColumnName("inactivo");

                entity.Property(e => e.RegsCounted)
                    .HasColumnType("datetime")
                    .HasColumnName("regsCounted")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RegsPadron)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("regsPadron")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<CatTiposUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipousuario)
                    .HasName("PK_Pad_Cat_Tiposusuario");

                entity.ToTable("Cat_TiposUsuario", "Padron");

                entity.HasIndex(e => e.Descripcion, "IX_Cat_TiposUsuario_Desc")
                    .IsUnique();

                entity.Property(e => e.IdTipousuario)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_tipousuario");

                entity.Property(e => e.CostoM3descarga)
                    .HasColumnType("money")
                    .HasColumnName("costo_m3descarga")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FactorDrenaje)
                    .HasColumnType("decimal(10, 4)")
                    .HasColumnName("factor_drenaje")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FactorTratamto)
                    .HasColumnType("numeric(10, 4)")
                    .HasColumnName("factor_tratamto")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ImpAguaFijo)
                    .HasColumnType("money")
                    .HasColumnName("imp_agua_fijo")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ImpDrenajeFijo)
                    .HasColumnType("money")
                    .HasColumnName("imp_drenaje_fijo")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ImpSaneamientoFijo)
                    .HasColumnType("money")
                    .HasColumnName("imp_saneamiento_fijo");

                entity.Property(e => e.Inactivo).HasColumnName("inactivo");

                entity.Property(e => e.MetrosBase)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("metros_base")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RegsCounted)
                    .HasColumnType("datetime")
                    .HasColumnName("regsCounted")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RegsPadron)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("regsPadron")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TasaIva)
                    .HasColumnType("decimal(10, 4)")
                    .HasColumnName("tasa_iva")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Tipo)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("tipo")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.VecesPromAlto)
                    .HasColumnType("decimal(10, 4)")
                    .HasColumnName("veces_prom_alto")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.VecesPromMenor)
                    .HasColumnType("decimal(10, 4)")
                    .HasColumnName("veces_prom_menor")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<VwCatPadron>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Cat_Padron", "Padron");

                entity.Property(e => e.Af)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("af");

                entity.Property(e => e.AltaFactura)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("_alta_factura");

                entity.Property(e => e.AnomaliaAct)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_anomalia_act");

                entity.Property(e => e.AnomaliaAnt)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_anomalia_ant");

                entity.Property(e => e.Anual).HasColumnName("anual");

                entity.Property(e => e.AreaConstruida)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("area_construida");

                entity.Property(e => e.AreaJardin)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("area_jardin");

                entity.Property(e => e.AreaLote)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("area_lote");

                entity.Property(e => e.Calculo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_calculo");

                entity.Property(e => e.CalculoAct)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_calculo_act");

                entity.Property(e => e.CalculoAnt)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_calculo_ant");

                entity.Property(e => e.CalleLat1)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("calle_lat1");

                entity.Property(e => e.CalleLat11)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_calle_lat1");

                entity.Property(e => e.CalleLat2)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("calle_lat2");

                entity.Property(e => e.CalleLat21)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_calle_lat2");

                entity.Property(e => e.CallePpal)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("calle_ppal");

                entity.Property(e => e.CallePpal1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_calle_ppal");

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("ciudad");

                entity.Property(e => e.Claseusuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_claseusuario");

                entity.Property(e => e.CodigoPostal)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("codigo_postal");

                entity.Property(e => e.Colonia)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("colonia");

                entity.Property(e => e.Colonia1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("_colonia");

                entity.Property(e => e.ConsumoAct)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("consumo_act");

                entity.Property(e => e.ConsumoAnt)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("consumo_ant");

                entity.Property(e => e.ConsumoFijo)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("consumo_fijo");

                entity.Property(e => e.ConsumoForzado)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("consumo_forzado");

                entity.Property(e => e.ConsumoRealAct)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("consumo_real_act");

                entity.Property(e => e.ConsumoRealAnt)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("consumo_real_ant");

                entity.Property(e => e.Curp)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("curp");

                entity.Property(e => e.Desviacion)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("desviacion");

                entity.Property(e => e.Diametro)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_diametro");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(356)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Email)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.EsAltoconsumidor).HasColumnName("es_altoconsumidor");

                entity.Property(e => e.EsDraef).HasColumnName("es_draef");

                entity.Property(e => e.EsFiscal).HasColumnName("es_fiscal");

                entity.Property(e => e.EsMacromedidor).HasColumnName("es_macromedidor");

                entity.Property(e => e.Estado)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Estatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_estatus");

                entity.Property(e => e.FechaAlta)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("_fecha_alta");

                entity.Property(e => e.FechaFacturaAct)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("_fecha_factura_act");

                entity.Property(e => e.FechaFacturaAnt)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("_fecha_factura_ant");

                entity.Property(e => e.FechaInsert)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("_fecha_insert");

                entity.Property(e => e.FechaLecturaAct)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("_fecha_lectura_act");

                entity.Property(e => e.FechaLecturaAnt)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("_fecha_lectura_ant");

                entity.Property(e => e.FechaVencimiento)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("_fecha_vencimiento");

                entity.Property(e => e.FechaVencimientoAct)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("_fecha_vencimiento_act");

                entity.Property(e => e.Fraccion)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("fraccion");

                entity.Property(e => e.Frente)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("frente");

                entity.Property(e => e.Giro)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("_giro");

                entity.Property(e => e.Hidrocircuito)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_hidrocircuito");

                entity.Property(e => e.IdAnomaliaAct)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("id_anomalia_act");

                entity.Property(e => e.IdClaseusuario)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("id_claseusuario");

                entity.Property(e => e.IdColonia)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("id_colonia");

                entity.Property(e => e.IdCuenta)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("id_cuenta");

                entity.Property(e => e.IdEstatus)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("id_estatus");

                entity.Property(e => e.IdGiro)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("id_giro");

                entity.Property(e => e.IdLocalidad)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("id_localidad");

                entity.Property(e => e.IdMedidor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("id_medidor");

                entity.Property(e => e.IdPadron)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("id_padron");

                entity.Property(e => e.IdServicio)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("id_servicio");

                entity.Property(e => e.IdSituacion)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("id_situacion");

                entity.Property(e => e.IdTarifa)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("id_tarifa");

                entity.Property(e => e.IdTarifafija)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("id_tarifafija");

                entity.Property(e => e.IdTipocalculo)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("id_tipocalculo");

                entity.Property(e => e.IdTipodraef)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("id_tipodraef");

                entity.Property(e => e.ImporteFijo)
                    .HasColumnType("money")
                    .HasColumnName("importe_fijo");

                entity.Property(e => e.ImporteFijoDren)
                    .HasColumnType("money")
                    .HasColumnName("importe_fijo_dren");

                entity.Property(e => e.ImporteFijoSane)
                    .HasColumnType("money")
                    .HasColumnName("importe_fijo_sane");

                entity.Property(e => e.Iva)
                    .HasColumnType("money")
                    .HasColumnName("iva");

                entity.Property(e => e.LastIdAbono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("last_idAbono");

                entity.Property(e => e.LastIdVenta)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("last_idVenta");

                entity.Property(e => e.Latitud)
                    .HasColumnType("decimal(14, 8)")
                    .HasColumnName("latitud");

                entity.Property(e => e.LecturaAct)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("lectura_act");

                entity.Property(e => e.LecturaAnt)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("lectura_ant");

                entity.Property(e => e.Localizacion)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("_localizacion");

                entity.Property(e => e.Longitud)
                    .HasColumnType("decimal(14, 8)")
                    .HasColumnName("longitud");

                entity.Property(e => e.Lote)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("lote");

                entity.Property(e => e.LpsPagados)
                    .HasColumnType("decimal(8, 4)")
                    .HasColumnName("lps_pagados");

                entity.Property(e => e.Manzana)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("manzana");

                entity.Property(e => e.MaterialBanqueta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_material_banqueta");

                entity.Property(e => e.MaterialCalle)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_material_calle");

                entity.Property(e => e.MaterialToma)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_material_toma");

                entity.Property(e => e.Materialmedidor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_materialmedidor");

                entity.Property(e => e.MesAdeudoAct)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("mes_adeudo_act");

                entity.Property(e => e.MesAdeudoAnt)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("mes_adeudo_ant");

                entity.Property(e => e.MesFacturado)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("_MesFacturado");

                entity.Property(e => e.Mf)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("mf");

                entity.Property(e => e.Nivel)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("nivel");

                entity.Property(e => e.Nivelsocial)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_nivelsocial");

                entity.Property(e => e.NomComercial)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("nom_comercial");

                entity.Property(e => e.NomPropietario)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("nom_propietario");

                entity.Property(e => e.NumExt)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("num_ext");

                entity.Property(e => e.NumInt)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("num_int");

                entity.Property(e => e.PaginaInternet)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("pagina_internet");

                entity.Property(e => e.Poblacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_poblacion");

                entity.Property(e => e.PorDescto)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("por_descto");

                entity.Property(e => e.Prefacturado).HasColumnName("prefacturado");

                entity.Property(e => e.PromedioAct)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("promedio_act");

                entity.Property(e => e.PromedioAnt)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("promedio_ant");

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("razon_social");

                entity.Property(e => e.Recibo)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("recibo");

                entity.Property(e => e.ReciboMail).HasColumnName("recibo_mail");

                entity.Property(e => e.Rfc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("rfc");

                entity.Property(e => e.Ruta)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("ruta");

                entity.Property(e => e.SalidasHidraulicas)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("salidas_hidraulicas");

                entity.Property(e => e.Sb)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("sb");

                entity.Property(e => e.Sector)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("sector");

                entity.Property(e => e.Servicio)
                    .HasMaxLength(50)
                    .HasColumnName("_servicio");

                entity.Property(e => e.Situacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_situacion");

                entity.Property(e => e.Subtotal)
                    .HasColumnType("money")
                    .HasColumnName("subtotal");

                entity.Property(e => e.Tarifafija)
                    .HasMaxLength(61)
                    .HasColumnName("_tarifafija");

                entity.Property(e => e.Telefono1)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("telefono1");

                entity.Property(e => e.Telefono2)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("telefono2");

                entity.Property(e => e.Telefono3)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("telefono3");

                entity.Property(e => e.TienePozo).HasColumnName("tiene_pozo");

                entity.Property(e => e.Tipodraef)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("_tipodraef");

                entity.Property(e => e.Tipofactible)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_tipofactible");

                entity.Property(e => e.Tipogrupo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_tipogrupo");

                entity.Property(e => e.Tipoinstalacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_tipoinstalacion");

                entity.Property(e => e.Tipotoma)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_tipotoma");

                entity.Property(e => e.Tipotuberia)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_tipotuberia");

                entity.Property(e => e.Tipousuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_tipousuario");

                entity.Property(e => e.Toma)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("toma");

                entity.Property(e => e.Total)
                    .HasColumnType("money")
                    .HasColumnName("total");

                entity.Property(e => e.Ubicacionmedidor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_ubicacionmedidor");

                entity.Property(e => e.Visitar).HasColumnName("visitar");

                entity.Property(e => e.Viviendas)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("viviendas");

                entity.Property(e => e.Zona)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("_zona");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
