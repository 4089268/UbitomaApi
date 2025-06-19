using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RutasApi.Models.Entities.Sicem;

namespace RutasApi.Data
{
    public partial class SicemContext : DbContext
    {
        public SicemContext()
        {
        }

        public SicemContext(DbContextOptions<SicemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CatDispositivo> CatDispositivos { get; set; } = null!;
        public virtual DbSet<Imagene> Imagenes { get; set; } = null!;
        public virtual DbSet<Operadore> Operadores { get; set; } = null!;
        public virtual DbSet<OprActualizacion> OprActualizacions { get; set; } = null!;
        public virtual DbSet<OprDetRutum> OprDetRuta { get; set; } = null!;
        public virtual DbSet<OprLoteNuevo> OprLoteNuevos { get; set; } = null!;
        public virtual DbSet<OprRutum> OprRuta { get; set; } = null!;
        public virtual DbSet<OprSesione> OprSesiones { get; set; } = null!;
        public virtual DbSet<Ruta> Rutas { get; set; } = null!;
        public virtual DbSet<SesionOperadore> SesionOperadores { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=Sicem");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Modern_Spanish_CI_AS");

            modelBuilder.Entity<CatDispositivo>(entity =>
            {
                entity.ToTable("cat_dispositivos", "moviles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Caduca)
                    .HasColumnType("date")
                    .HasColumnName("caduca");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdUsuario)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("id_usuario");

                entity.Property(e => e.Imei)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("imei");

                entity.Property(e => e.Inactivo).HasColumnName("inactivo");

                entity.Property(e => e.Oficina).HasColumnName("oficina");
            });

            modelBuilder.Entity<Imagene>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Fecha)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("fecha");

                entity.Property(e => e.FechaInsercion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_insercion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Imagen)
                    .HasColumnType("image")
                    .HasColumnName("imagen");

                entity.Property(e => e.Oficina).HasColumnName("oficina");

                entity.Property(e => e.Opcion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("opcion");
            });

            modelBuilder.Entity<Operadore>(entity =>
            {
                entity.ToTable("Operadores", "Ubitoma");

                entity.HasIndex(e => e.Usuario, "UQ__Operador__9AFF8FC6B0798C7F")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Password)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.RutaId).HasColumnName("rutaID");

                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("usuario");

                entity.HasOne(d => d.Ruta)
                    .WithMany(p => p.Operadores)
                    .HasForeignKey(d => d.RutaId)
                    .HasConstraintName("FK_UbitomaOperadores_Ruta");
            });

            modelBuilder.Entity<OprActualizacion>(entity =>
            {
                entity.ToTable("Opr_Actualizacion", "Ubitoma");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnomaliaPredio)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("anomaliaPredio");

                entity.Property(e => e.Calle)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("calle");

                entity.Property(e => e.CalleLat1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("calleLat1");

                entity.Property(e => e.CalleLat2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("calleLat2");

                entity.Property(e => e.Colonia)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("colonia");

                entity.Property(e => e.DiametroToma)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("diametroToma");

                entity.Property(e => e.Estatus)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("estatus");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Giro)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("giro");

                entity.Property(e => e.IdActualizacionAnt).HasColumnName("idActualizacionAnt");

                entity.Property(e => e.IdAnomaliaPredio).HasColumnName("idAnomaliaPredio");

                entity.Property(e => e.IdCuenta).HasColumnName("idCuenta");

                entity.Property(e => e.IdEstatus).HasColumnName("idEstatus");

                entity.Property(e => e.IdGiro).HasColumnName("idGiro");

                entity.Property(e => e.IdOficina).HasColumnName("idOficina");

                entity.Property(e => e.IdPadron).HasColumnName("idPadron");

                entity.Property(e => e.IdSituacion).HasColumnName("idSituacion");

                entity.Property(e => e.IdTarifa).HasColumnName("idTarifa");

                entity.Property(e => e.Latitud)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("latitud");

                entity.Property(e => e.Longitud)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("longitud");

                entity.Property(e => e.MarcaMedidor)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("marcaMedidor");

                entity.Property(e => e.NumeroExt)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("numeroExt");

                entity.Property(e => e.NumeroInt)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("numeroInt");

                entity.Property(e => e.NumeroMedidor)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("numeroMedidor");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("observaciones");

                entity.Property(e => e.OperadorId).HasColumnName("operadorID");

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("razonSocial");

                entity.Property(e => e.SectorHidraulico)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("sectorHidraulico");

                entity.Property(e => e.Situacion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("situacion");

                entity.Property(e => e.Tarifa)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tarifa");

                entity.Property(e => e.TienePozo).HasColumnName("tienePozo");

                entity.Property(e => e.TipoInstalacion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tipoInstalacion");

                entity.Property(e => e.TipoToma)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tipoToma");

                entity.HasOne(d => d.IdOficinaNavigation)
                    .WithMany(p => p.OprActualizacions)
                    .HasForeignKey(d => d.IdOficina)
                    .HasConstraintName("FK_RutaID");

                entity.HasOne(d => d.Operador)
                    .WithMany(p => p.OprActualizacions)
                    .HasForeignKey(d => d.OperadorId)
                    .HasConstraintName("FK_ActualizacionOperador");
            });

            modelBuilder.Entity<OprDetRutum>(entity =>
            {
                entity.ToTable("Opr_DetRuta", "Ubitoma");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cuenta).HasColumnName("cuenta");

                entity.Property(e => e.IdPadron).HasColumnName("id_padron");

                entity.Property(e => e.OprRutaId).HasColumnName("OprRutaID");

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("razon_social");

                entity.HasOne(d => d.OprRuta)
                    .WithMany(p => p.OprDetRuta)
                    .HasForeignKey(d => d.OprRutaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Opr_DetRu__OprRu__5614BF03");
            });

            modelBuilder.Entity<OprLoteNuevo>(entity =>
            {
                entity.ToTable("Opr_LoteNuevo", "Ubitoma");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnomaliaPredio)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("anomaliaPredio");

                entity.Property(e => e.Calle)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("calle");

                entity.Property(e => e.CalleLat1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("calleLat1");

                entity.Property(e => e.CalleLat2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("calleLat2");

                entity.Property(e => e.Colonia)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("colonia");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Giro)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("giro");

                entity.Property(e => e.IdAnomaliaPredio).HasColumnName("idAnomaliaPredio");

                entity.Property(e => e.IdGiro).HasColumnName("idGiro");

                entity.Property(e => e.IdOficina).HasColumnName("idOficina");

                entity.Property(e => e.IdPadron).HasColumnName("idPadron");

                entity.Property(e => e.Latitud)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("latitud");

                entity.Property(e => e.Longitud)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("longitud");

                entity.Property(e => e.NumeroExt)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("numeroExt");

                entity.Property(e => e.NumeroInt)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("numeroInt");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("observaciones");

                entity.Property(e => e.OperadorId).HasColumnName("operadorID");

                entity.Property(e => e.TipoInstalacion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tipoInstalacion");

                entity.Property(e => e.TipoToma)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tipoToma");

                entity.HasOne(d => d.IdOficinaNavigation)
                    .WithMany(p => p.OprLoteNuevos)
                    .HasForeignKey(d => d.IdOficina)
                    .HasConstraintName("FK_LoteNuevoRuta");

                entity.HasOne(d => d.Operador)
                    .WithMany(p => p.OprLoteNuevos)
                    .HasForeignKey(d => d.OperadorId)
                    .HasConstraintName("FK_LoteNuevoOperador");
            });

            modelBuilder.Entity<OprRutum>(entity =>
            {
                entity.ToTable("Opr_Ruta", "Ubitoma");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.RutaId).HasColumnName("rutaID");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.Ruta)
                    .WithMany(p => p.OprRuta)
                    .HasForeignKey(d => d.RutaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Opr_Ruta__rutaID__53385258");
            });

            modelBuilder.Entity<OprSesione>(entity =>
            {
                entity.ToTable("Opr_Sesiones", "Global");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Caducidad)
                    .HasColumnType("datetime")
                    .HasColumnName("caducidad");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Inicio)
                    .HasColumnType("datetime")
                    .HasColumnName("inicio");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ip_address");

                entity.Property(e => e.MacAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("mac_address");
            });

            modelBuilder.Entity<Ruta>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Alias)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("alias");

                entity.Property(e => e.Alterno).HasColumnName("alterno");

                entity.Property(e => e.BaseDatos)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Base_Datos");

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Desconectado)
                    .HasColumnName("desconectado")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IdZona).HasColumnName("id_zona");

                entity.Property(e => e.Inactivo).HasDefaultValueSql("((0))");

                entity.Property(e => e.Oficina)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("oficina");

                entity.Property(e => e.Ruta1)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("ruta");

                entity.Property(e => e.Servidor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ServidorA)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Servidor_A");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SesionOperadore>(entity =>
            {
                entity.ToTable("Sesion_Operadores", "Ubitoma");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateAt).HasColumnName("createAt");

                entity.Property(e => e.OperadorId).HasColumnName("operadorID");

                entity.Property(e => e.ValidTo).HasColumnName("validTo");

                entity.HasOne(d => d.Operador)
                    .WithMany(p => p.SesionOperadores)
                    .HasForeignKey(d => d.OperadorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SesionOperadores_OperadoresID");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(e => e.Usuario1, "IX_Usuarios")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Administrador)
                    .HasColumnName("administrador")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CfgOfi)
                    .HasColumnName("cfg_ofi")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CfgOpc)
                    .HasColumnName("cfg_opc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("contraseña");

                entity.Property(e => e.Inactivo)
                    .HasColumnName("inactivo")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Oficinas)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("oficinas");

                entity.Property(e => e.Opciones)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("opciones");

                entity.Property(e => e.UltimaModif).HasColumnType("datetime");

                entity.Property(e => e.Usuario1)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
