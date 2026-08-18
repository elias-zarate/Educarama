using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Datos.EntidadesDb;

public partial class EducaramaContext : DbContext
{
    public EducaramaContext()
    {
    }

    public EducaramaContext(DbContextOptions<EducaramaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accion> Acciones { get; set; }
    public virtual DbSet<AsignacionRol> AsignacionesRol { get; set; }
    public virtual DbSet<Aviso> Avisos { get; set; }
    public virtual DbSet<CategoriaRama> CategoriasRama { get; set; }
    public virtual DbSet<Competencia> Competencias { get; set; }
    public virtual DbSet<Cumple> Cumplimientos { get; set; }
    public virtual DbSet<Desarrolla> Desarrollos { get; set; }
    public virtual DbSet<Documento> Documentos { get; set; }
    public virtual DbSet<EsFamilia> RelacionesFamilia { get; set; }
    public virtual DbSet<Familiar> Familiares { get; set; }
    public virtual DbSet<Grupo> Grupos { get; set; }
    public virtual DbSet<Insignia> Insignias { get; set; }
    public virtual DbSet<Obtiene> Obtenciones { get; set; }
    public virtual DbSet<Patrulla> Patrullas { get; set; }
    public virtual DbSet<Persona> Personas { get; set; }
    public virtual DbSet<Protagonista> Protagonistas { get; set; }
    public virtual DbSet<Rama> Ramas { get; set; }
    public virtual DbSet<Rol> Roles { get; set; }
    public virtual DbSet<Sendero> Senderos { get; set; }
    public virtual DbSet<TipoAccion> TiposAccion { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__accion__3213E83FF557E7F7");

            entity.ToTable("accion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CodigoCompetencia).HasColumnName("codigo_competencia");
            entity.Property(e => e.Descripcion)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdCompetencia).HasColumnName("id_competencia");
            entity.Property(e => e.IdTipoAccion).HasColumnName("id_tipo_accion");
        });

        modelBuilder.Entity<AsignacionRol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__asignaci__3213E83F78780251");

            entity.ToTable("asignacion_rol");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdGrupo).HasColumnName("id_grupo");
            entity.Property(e => e.IdRama).HasColumnName("id_rama");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdGrupoNavigation).WithMany(p => p.AsignacionRol)
                .HasForeignKey(d => d.IdGrupo)
                .HasConstraintName("asignacion_rol_ibfk_3");

            entity.HasOne(d => d.IdRamaNavigation).WithMany(p => p.AsignacionRol)
                .HasForeignKey(d => d.IdRama)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("asignacion_rol_ibfk_4");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.AsignacionRol)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("asignacion_rol_ibfk_2");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.AsignacionRol)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("asignacion_rol_ibfk_1");
        });

        modelBuilder.Entity<Aviso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__aviso__3213E83F22292FC9");

            entity.ToTable("aviso");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.Prioridad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("prioridad");
            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("titulo");
        });

        modelBuilder.Entity<CategoriaRama>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83F378086E4");

            entity.ToTable("categoria_rama");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Competencia>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.Codigo }).HasName("PK__competen__561C721FEC519B08");

            entity.ToTable("competencia");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdCategoriaRama).HasColumnName("id_categoria_rama");
            entity.Property(e => e.IdSendero).HasColumnName("id_sendero");
        });

        modelBuilder.Entity<Cumple>(entity =>
        {
            entity.HasKey(e => new { e.IdProtagonista, e.IdAccion }).HasName("PK__cumple__293224432089CAB1");

            entity.ToTable("cumple");

            entity.Property(e => e.IdProtagonista).HasColumnName("id_protagonista");
            entity.Property(e => e.IdAccion).HasColumnName("id_accion");
            entity.Property(e => e.AccionCumplida).HasColumnName("accion_cumplida");
        });

        modelBuilder.Entity<Desarrolla>(entity =>
        {
            entity.HasKey(e => new { e.IdProtagonista, e.IdCompetencia, e.CodigoCompetencia }).HasName("PK__desarrol__89C89280E142FDF5");

            entity.ToTable("desarrolla");

            entity.Property(e => e.IdProtagonista).HasColumnName("id_protagonista");
            entity.Property(e => e.IdCompetencia).HasColumnName("id_competencia");
            entity.Property(e => e.CodigoCompetencia).HasColumnName("codigo_competencia");
            entity.Property(e => e.CompetenciaDesarrollada).HasColumnName("competencia_desarrollada");
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__document__3213E83FDB0A9E6C");

            entity.ToTable("documento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdProtagonista).HasColumnName("id_protagonista");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("tipo_documento");
        });

        modelBuilder.Entity<EsFamilia>(entity =>
        {
            entity.HasKey(e => new { e.IdProtagonista, e.IdFamiliar }).HasName("PK__es_famil__7C630FDE6DBE56CF");

            entity.ToTable("es_familia");

            entity.Property(e => e.IdProtagonista).HasColumnName("id_protagonista");
            entity.Property(e => e.IdFamiliar).HasColumnName("id_familiar");
            entity.Property(e => e.EsContactoDeEmergencia).HasColumnName("es_contacto_de_emergencia");
            entity.Property(e => e.Vinculo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("vinculo");
        });

        modelBuilder.Entity<Familiar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__familiar__3213E83F71FCCA89");

            entity.ToTable("familiar");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmailContacto)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("email_contacto");
            entity.Property(e => e.IdPersona).HasColumnName("id_persona");
            entity.Property(e => e.TelefonoContacto).HasColumnName("telefono_contacto");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__grupo__3213E83F054767E3");

            entity.ToTable("grupo");

            entity.HasIndex(e => e.Codigo, "UQ__grupo__40F9A206EA72361E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Insignia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__insignia__3213E83FABF47C1D");

            entity.ToTable("insignia");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCategoriaRama).HasColumnName("id_categoria_rama");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Obtiene>(entity =>
        {
            entity.HasKey(e => new { e.IdProtagonista, e.IdInsignia }).HasName("PK__obtiene__4A5A2718F4BA585B");

            entity.ToTable("obtiene");

            entity.Property(e => e.IdProtagonista).HasColumnName("id_protagonista");
            entity.Property(e => e.IdInsignia).HasColumnName("id_insignia");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
        });

        modelBuilder.Entity<Patrulla>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__patrulla__3213E83FEE922D8F");

            entity.ToTable("patrulla");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdRama).HasColumnName("id_rama");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__persona__3213E83FAAB0B0E6");

            entity.ToTable("persona");

            entity.HasIndex(e => e.Dni, "UQ__persona__D87608A706A437D6").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Dni).HasColumnName("dni");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Protagonista>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__protagon__3213E83F04C159BC");

            entity.ToTable("protagonista");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdPatrulla).HasColumnName("id_patrulla");
            entity.Property(e => e.IdPersona).HasColumnName("id_persona");
            entity.Property(e => e.IdRama).HasColumnName("id_rama");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Promesa).HasColumnName("promesa");
        });

        modelBuilder.Entity<Rama>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rama__3213E83FCB09A0AD");

            entity.ToTable("rama");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCategoriaRama).HasColumnName("id_categoria_rama");
            entity.Property(e => e.IdGrupo).HasColumnName("id_grupo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdCategoriaRamaNavigation).WithMany(p => p.Rama)
                .HasForeignKey(d => d.IdCategoriaRama)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rama_CategoriaRama");

            entity.HasOne(d => d.IdGrupoNavigation).WithMany(p => p.Rama)
                .HasForeignKey(d => d.IdGrupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rama_ibfk_1");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rol__3213E83F0ABF9BC3");

            entity.ToTable("rol");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Sendero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sendero__3213E83FA361E957");

            entity.ToTable("sendero");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Tipo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<TipoAccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tipo_acc__3213E83FBA8C7911");

            entity.ToTable("tipo_accion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Tipo)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuario__3213E83F92017257");

            entity.ToTable("usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contrasenia");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
