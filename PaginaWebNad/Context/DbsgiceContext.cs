using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PaginaWebNad.Models;

public partial class DbsgiceContext : DbContext
{
    public DbsgiceContext()
    {
    }

    public DbsgiceContext(DbContextOptions<DbsgiceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CSistema> CSistemas { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<CtRole> CtRoles { get; set; }

    public virtual DbSet<TErpgrupo> TErpgrupos { get; set; }

    public virtual DbSet<TErpgrupoSistema> TErpgrupoSistemas { get; set; }

    public virtual DbSet<TUsuario> TUsuarios { get; set; }

    public virtual DbSet<TUsuarioSistemaRol> TUsuarioSistemaRols { get; set; }

    public virtual DbSet<TrRolesSistema> TrRolesSistemas { get; set; }

    public virtual DbSet<TrRolesSistemaUsuariosGrupo> TrRolesSistemaUsuariosGrupos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MARCO\\SQLEXPRESS; Database=DBSGICE; Trusted_Connection=True; TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CSistema>(entity =>
        {
            entity.HasKey(e => e.IdSistema).HasName("PK__cSistema__EB7CAC5C8D1BCEB7");

            entity.ToTable("cSistemas");

            entity.Property(e => e.IdSistema).HasColumnName("idSistema");
            entity.Property(e => e.NomSistema)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nomSistema");
            entity.Property(e => e.Nomenglatura)
                .HasMaxLength(255)
                .HasColumnName("nomenglatura");
            entity.Property(e => e.Url)
                .HasMaxLength(800)
                .IsUnicode(false)
                .HasColumnName("url");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IIdCliente).HasName("PK__Clientes__4504E24034D0090C");

            entity.Property(e => e.IIdCliente).HasColumnName("iIdCliente");
            entity.Property(e => e.SNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sNombre");
        });

        modelBuilder.Entity<CtRole>(entity =>
        {
            entity.HasKey(e => e.IIdRol).HasName("PK__ctRoles__87A2FA5D86C08918");

            entity.ToTable("ctRoles");

            entity.Property(e => e.IIdRol).HasColumnName("iIdRol");
            entity.Property(e => e.IEstatus).HasColumnName("iEstatus");
            entity.Property(e => e.SDescripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("sDescripcion");
            entity.Property(e => e.SRol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sRol");
        });

        modelBuilder.Entity<TErpgrupo>(entity =>
        {
            entity.HasKey(e => e.IdErpgrupo).HasName("PK__tERPGrup__C010ACF56BF5B38B");

            entity.ToTable("tERPGrupo");

            entity.Property(e => e.IdErpgrupo).HasColumnName("idERPGrupo");
            entity.Property(e => e.IIdCliente).HasColumnName("iIdCliente");
            entity.Property(e => e.NomGrupo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nomGrupo");
            entity.Property(e => e.UrlErp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("urlERP");

            entity.HasOne(d => d.IIdClienteNavigation).WithMany(p => p.TErpgrupos)
                .HasForeignKey(d => d.IIdCliente)
                .HasConstraintName("FK__tERPGrupo__iIdCl__5DCAEF64");
        });

        modelBuilder.Entity<TErpgrupoSistema>(entity =>
        {
            entity.HasKey(e => e.IdErpgrupoSistema).HasName("PK__tERPGrup__11F9E96B535F0C07");

            entity.ToTable("tERPGrupoSistema");

            entity.Property(e => e.IdErpgrupoSistema).HasColumnName("idERPGrupoSistema");
            entity.Property(e => e.AppCreada).HasColumnName("appCreada");
            entity.Property(e => e.IdErpgrupo).HasColumnName("idERPGrupo");
            entity.Property(e => e.IdSistema).HasColumnName("idSistema");
            entity.Property(e => e.SNombreAdicional)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sNombreAdicional");
            entity.Property(e => e.ScriptCreado).HasColumnName("scriptCreado");

            entity.HasOne(d => d.IdErpgrupoNavigation).WithMany(p => p.TErpgrupoSistemas)
                .HasForeignKey(d => d.IdErpgrupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tERPGrupo__idERP__5535A963");

            entity.HasOne(d => d.IdSistemaNavigation).WithMany(p => p.TErpgrupoSistemas)
                .HasForeignKey(d => d.IdSistema)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tERPGrupo__idSis__5629CD9C");
        });

        modelBuilder.Entity<TUsuario>(entity =>
        {
            entity.HasKey(e => e.IIdUsuario).HasName("PK__tUsuario__A271047F0F3AD2F0");

            entity.ToTable("tUsuarios");

            entity.Property(e => e.IIdUsuario).HasColumnName("iIdUsuario");
            entity.Property(e => e.IEstatus).HasColumnName("iEstatus");
            entity.Property(e => e.SCorreo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sCorreo");
            entity.Property(e => e.SUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sUsuario");
        });

        modelBuilder.Entity<TUsuarioSistemaRol>(entity =>
        {
            entity.HasKey(e => e.IIdUsuarioSistemaRol).HasName("PK__tUsuario__6949B915360FE947");

            entity.ToTable("tUsuarioSistemaRol");

            entity.Property(e => e.IIdUsuarioSistemaRol).HasColumnName("iIdUsuarioSistemaRol");
            entity.Property(e => e.IIdRol).HasColumnName("iIdRol");
            entity.Property(e => e.IIdUsuario).HasColumnName("iIdUsuario");
            entity.Property(e => e.IdSistema).HasColumnName("idSistema");

            entity.HasOne(d => d.IIdRolNavigation).WithMany(p => p.TUsuarioSistemaRols)
                .HasForeignKey(d => d.IIdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tUsuarioS__iIdRo__628FA481");

            entity.HasOne(d => d.IIdUsuarioNavigation).WithMany(p => p.TUsuarioSistemaRols)
                .HasForeignKey(d => d.IIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tUsuarioS__iIdUs__60A75C0F");

            entity.HasOne(d => d.IdSistemaNavigation).WithMany(p => p.TUsuarioSistemaRols)
                .HasForeignKey(d => d.IdSistema)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tUsuarioS__idSis__619B8048");
        });

        modelBuilder.Entity<TrRolesSistema>(entity =>
        {
            entity.HasKey(e => e.IIdRolSistema).HasName("PK__trRolesS__F5115EFC7053C7D7");

            entity.ToTable("trRolesSistema");

            entity.Property(e => e.IIdRolSistema).HasColumnName("iIdRolSistema");
            entity.Property(e => e.IIdRol).HasColumnName("iIdRol");
            entity.Property(e => e.IdSistema).HasColumnName("idSistema");

            entity.HasOne(d => d.IIdRolNavigation).WithMany(p => p.TrRolesSistemas)
                .HasForeignKey(d => d.IIdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__trRolesSi__iIdRo__5165187F");

            entity.HasOne(d => d.IdSistemaNavigation).WithMany(p => p.TrRolesSistemas)
                .HasForeignKey(d => d.IdSistema)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__trRolesSi__idSis__52593CB8");
        });

        modelBuilder.Entity<TrRolesSistemaUsuariosGrupo>(entity =>
        {
            entity.HasKey(e => e.IIdRolesSistemaUsuarios).HasName("PK__trRolesS__A45FCDD9D38984D4");

            entity.ToTable("trRolesSistemaUsuariosGrupo");

            entity.Property(e => e.IIdRolesSistemaUsuarios).HasColumnName("iIdRolesSistemaUsuarios");
            entity.Property(e => e.IIdRolSistema).HasColumnName("iIdRolSistema");
            entity.Property(e => e.IdErpgrupoSistema).HasColumnName("idERPGrupoSistema");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            entity.HasOne(d => d.IIdRolSistemaNavigation).WithMany(p => p.TrRolesSistemaUsuariosGrupos)
                .HasForeignKey(d => d.IIdRolSistema)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__trRolesSi__iIdRo__59063A47");

            entity.HasOne(d => d.IdErpgrupoSistemaNavigation).WithMany(p => p.TrRolesSistemaUsuariosGrupos)
                .HasForeignKey(d => d.IdErpgrupoSistema)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__trRolesSi__idERP__59FA5E80");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TrRolesSistemaUsuariosGrupos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__trRolesSi__idUsu__5AEE82B9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
