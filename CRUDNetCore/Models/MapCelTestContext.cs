using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRUDNetCore.Models
{
    public partial class MapCelTestContext : DbContext
    {
        public MapCelTestContext()
        {
        }

        public MapCelTestContext(DbContextOptions<MapCelTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Inventario> Inventario { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<SubCategoria> SubCategoria { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=MARCUSHOST;Database=MapCelTest;Trusted_Connection=True; User ID=sa; Password=Marcus_988;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.UnqGencategoriaKey);

                entity.ToTable("Categoria", "GEN");

                entity.Property(e => e.UnqGencategoriaKey)
                    .HasColumnName("unqGENCategoriaKey")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.VchCodigo)
                    .IsRequired()
                    .HasColumnName("vchCodigo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.VchDescripcion)
                    .IsRequired()
                    .HasColumnName("vchDescripcion")
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.HasKey(e => e.UnqInvinventarioKey);

                entity.ToTable("Inventario", "INV");

                entity.Property(e => e.UnqInvinventarioKey)
                    .HasColumnName("unqINVInventarioKey")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.IntCantidad).HasColumnName("intCantidad");

                entity.Property(e => e.UnqGenproductoLink).HasColumnName("unqGENProductoLink");

                entity.Property(e => e.VchNumeroSerie)
                    .IsRequired()
                    .HasColumnName("vchNumeroSerie")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VchSku)
                    .IsRequired()
                    .HasColumnName("vchSKU")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UnqGenproductoLinkNavigation)
                    .WithMany(p => p.Inventario)
                    .HasForeignKey(d => d.UnqGenproductoLink)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INVInventario_GENProducto");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.UnqGenproductoKey);

                entity.ToTable("Producto", "GEN");

                entity.Property(e => e.UnqGenproductoKey)
                    .HasColumnName("unqGENProductoKey")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.UnqGensubCategoriaLink).HasColumnName("unqGENSubCategoriaLink");

                entity.Property(e => e.VchDescripcion)
                    .IsRequired()
                    .HasColumnName("vchDescripcion")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.UnqGensubCategoriaLinkNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.UnqGensubCategoriaLink)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GENProducto_GENSubCategoria");
            });

            modelBuilder.Entity<SubCategoria>(entity =>
            {
                entity.HasKey(e => e.UnqGensubCategoriaKey);

                entity.ToTable("SubCategoria", "GEN");

                entity.Property(e => e.UnqGensubCategoriaKey)
                    .HasColumnName("unqGENSubCategoriaKey")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.UnqGencategoriaLink).HasColumnName("unqGENCategoriaLink");

                entity.Property(e => e.VchCodigo)
                    .IsRequired()
                    .HasColumnName("vchCodigo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.VchDescripcion)
                    .IsRequired()
                    .HasColumnName("vchDescripcion")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.UnqGencategoriaLinkNavigation)
                    .WithMany(p => p.SubCategoria)
                    .HasForeignKey(d => d.UnqGencategoriaLink)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GENSubCategoria_GENCategoria");
            });
        }
    }
}
