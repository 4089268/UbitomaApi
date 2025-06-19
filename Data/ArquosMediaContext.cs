using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RutasApi.Models.Entities.ArquosMedia;

#nullable disable

namespace RutasApi.Data {
    public partial class ArquosMediaContext : DbContext {
        private readonly string cadConexion;
        public ArquosMediaContext(string c) {
            this.cadConexion = c;
        }

        public ArquosMediaContext(DbContextOptions<ArquosMediaContext> options) : base(options) {
        }

        public virtual DbSet<OprImagene> OprImagenes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(cadConexion);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<OprImagene>(entity =>
            {
                entity.HasKey(e => new { e.Tabla, e.IdTabla, e.IdImagen })
                    .HasName("PK_opr_imagenes");

                entity.ToTable("Opr_Imagenes", "Global");

                entity.Property(e => e.Tabla)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("tabla")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.IdTabla)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("id_tabla")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.IdImagen)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_imagen");

                entity.Property(e => e.Archivo)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("archivo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Documento).HasColumnName("documento");

                entity.Property(e => e.FechaInsert)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_insert")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FileExtension)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("file_extension")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Filesize)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("filesize")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IdInsert)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_insert")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.IdMediatype)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("id_mediatype")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdPadron)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("id_padron")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Imagen)
                    .HasColumnType("image")
                    .HasColumnName("imagen");

                entity.Property(e => e.Maquina)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("maquina")
                    .HasDefaultValueSql("('')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
