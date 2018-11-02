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
                entity.ToTable("Categoria", "GEN");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.ToTable("Inventario", "INV");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Idproducto).HasColumnName("IDProducto");

                entity.Property(e => e.NumeroSerie)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Sku)
                    .IsRequired()
                    .HasColumnName("SKU")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.Inventario)
                    .HasForeignKey(d => d.Idproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INVInventario_GENProducto");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Producto", "GEN");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.IdsubCategoria).HasColumnName("IDSubCategoria");

                entity.HasOne(d => d.IdsubCategoriaNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.IdsubCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GENProducto_GENSubCategoria");
            });

            modelBuilder.Entity<SubCategoria>(entity =>
            {
                entity.ToTable("SubCategoria", "GEN");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Idcategoria).HasColumnName("IDCategoria");

                entity.HasOne(d => d.IdcategoriaNavigation)
                    .WithMany(p => p.SubCategoria)
                    .HasForeignKey(d => d.Idcategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GENSubCategoria_GENCategoria");
            });
        }
    }
}
