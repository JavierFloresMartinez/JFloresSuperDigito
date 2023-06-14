using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class JfloresSuperDigitoContext : DbContext
{
    public JfloresSuperDigitoContext()
    {
    }

    public JfloresSuperDigitoContext(DbContextOptions<JfloresSuperDigitoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Registro> Registros { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= JFloresSuperDigito; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Registro>(entity =>
        {
            entity.HasKey(e => e.IdDigito).HasName("PK__Registro__703792F46A2A5037");

            entity.ToTable("Registro");

            entity.Property(e => e.FechaYhora)
                .HasColumnType("datetime")
                .HasColumnName("FechaYHora");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Registros)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Registro__IdUsua__239E4DCF");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF9747D62FC5");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Username, "UQ__Usuario__536C85E465130BDD").IsUnique();

            entity.Property(e => e.Password).HasMaxLength(1);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
