using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DL
{
    public partial class ItalikaContext : DbContext
    {
        public ItalikaContext()
        {
        }

        public ItalikaContext(DbContextOptions<ItalikaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Motocicletum> Motocicleta { get; set; } = null!;
        public virtual DbSet<Tipo> Tipos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.; Database= Italika; Trusted_Connection=True; User ID=sa; Password=pass@word1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Motocicletum>(entity =>
            {
                entity.HasKey(e => e.IdMotocicleta)
                    .HasName("PK__Motocicl__82407148F9BA7CB8");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Fert)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Imagen).IsUnicode(false);

                entity.Property(e => e.Modelo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NúmeroDeSerie)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sku)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SKU");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Motocicleta)
                    .HasForeignKey(d => d.IdTipo)
                    .HasConstraintName("FK__Motocicle__IdTip__1A14E395");
            });

            modelBuilder.Entity<Tipo>(entity =>
            {
                entity.HasKey(e => e.IdTipo)
                    .HasName("PK__Tipo__9E3A29A5E1D6A1EF");

                entity.ToTable("Tipo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
