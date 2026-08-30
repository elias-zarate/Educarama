using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Acciones.CasosDeUso;
using Aplicacion.Acciones.DTOs;
using Aplicacion.Acciones.Mappers;
using Aplicacion.CategoriaRamas.CasosDeUso;
using Aplicacion.CategoriaRamas.DTOs;
using Aplicacion.CategoriaRamas.Mappers;
using Aplicacion.Competencias.CasosDeUso;
using Aplicacion.Competencias.DTOs;
using Aplicacion.Competencias.Mappers;
using Aplicacion.Cumple.CasosDeUso;
using Aplicacion.Cumple.DTOs;
using Aplicacion.Cumple.Mappers;
using Aplicacion.Desarrolla.CasosDeUso;
using Aplicacion.Desarrolla.DTOs;
using Aplicacion.Familiares.CasosDeUso;
using Aplicacion.Familiares.DTOs;
using Aplicacion.Grupos.CasosDeUso;
using Aplicacion.Grupos.DTOs;
using Aplicacion.Insignias.CasosDeUso;
using Aplicacion.Insignias.DTOs;
using Aplicacion.Obtiene.DTOs;
using Aplicacion.Obtiene.Mappers;
using Aplicacion.Patrullas.CasosDeUso;
using Aplicacion.Patrullas.DTOs;
using Aplicacion.Patrullas.Mappers;
using Aplicacion.Protagonistas.CasosDeUso;
using Aplicacion.Protagonistas.DTOs;
using Aplicacion.Protagonistas.Mappers;
using Aplicacion.Ramas.CasosDeUso;
using Aplicacion.Ramas.DTOs;
using Aplicacion.Ramas.Mappers;
using Aplicacion.Usuarios.CasosDeUso;
using Aplicacion.Usuarios.DTOs;
using Aplicacion.Usuarios.Mappers;
using Datos.EntidadesDb;
using Dominio;
using Microsoft.EntityFrameworkCore;
using Repositorio;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Area de servicios
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // <-- Swagger/OpenAPI y Scalar

// Configuración de CORS para permitir peticiones del Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var cadenaDeConexion = builder.Configuration.GetConnectionString("DefaultConnection");

// ===============================================
// Servicios de Categoría de Ramas
// ===============================================
builder.Services.AddScoped<ILeerRepositorio<CategoriaRamaEntity>, CategoriaRamaRepositorio>();
builder.Services.AddScoped<ILeerUseCase<CategoriaRamaDTO>, ObtenerCategoriaRamasUseCase>();
builder.Services.AddScoped<IMapper<CategoriaRamaEntity, CategoriaRamaDTO>, CategoriaRamaEntityToDtoMapper>();

// ===============================================
// Servicios de Ramas
// ===============================================
builder.Services.AddScoped<ILeerRepositorio<RamaEntity>, RamaRepositorio>();
builder.Services.AddScoped<IActualizarRepositorio<RamaEntity>, RamaRepositorio>();
builder.Services.AddScoped<ILeerUseCase<RamaDTO>, ObtenerRamasUseCase>();
builder.Services.AddScoped<GenerarCodigoAccesoRamaUseCase>();

// ===============================================
// Servicios de Patrullas
// ===============================================
builder.Services.AddScoped<ILeerRepositorio<PatrullaEntity>, PatrullaRepositorio>();
builder.Services.AddScoped<ILeerPatrullasPorRamaRepositorio, PatrullaRepositorio>();
builder.Services.AddScoped<ICrearRepositorio<PatrullaEntity>, PatrullaRepositorio>();
builder.Services.AddScoped<IActualizarRepositorio<PatrullaEntity>, PatrullaRepositorio>();
builder.Services.AddScoped<IMapper<PatrullaEntity, PatrullaDTO>, PatrullaEntityToDtoMapper>();
builder.Services.AddScoped<ILeerUseCase<PatrullaDTO>, ObtenerPatrullasUseCase>();
builder.Services.AddScoped<ILeerPatrullasPorRamaUseCase, ObtenerPatrullasPorRamaUseCase>();
builder.Services.AddScoped<ICrearUseCase<RegistroPatrullaDTO>, RegistrarPatrullaUseCase>();
builder.Services.AddScoped<IActualizarUseCase<PatrullaDTO>, ActualizarPatrullaUseCase>();
builder.Services.AddScoped<IEliminarUseCase, EliminarPatrullaUseCase>();

// ===============================================
// Servicios de Competencias y Acciones
// ===============================================
builder.Services.AddScoped<ILeerRepositorio<CompetenciaEntity>, CompetenciaRepositorio>();
builder.Services.AddScoped<ILeerRepositorio<AccionEntity>, AccionRepositorio>();
builder.Services.AddScoped<IMapper<CompetenciaEntity, CompetenciaDTO>, CompetenciaEntityToDtoMapper>();
builder.Services.AddScoped<IMapper<AccionEntity, AccionDTO>, AccionEntityToDtoMapper>();
builder.Services.AddScoped<ILeerUseCase<CompetenciaDTO>, ObtenerCompetenciasUseCase>();
builder.Services.AddScoped<ILeerUseCase<AccionDTO>, ObtenerAccionesUseCase>();

// ===============================================
// Servicios de Usuario / Auth (Login y Registro)
// ===============================================
builder.Services.AddScoped<ILeerUsuarioPorEmailRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ICrearRepositorio<UsuarioEntity>, UsuarioRepositorio>();
builder.Services.AddScoped<ILeerRepositorio<UsuarioEntity>, UsuarioRepositorio>();
builder.Services.AddScoped<IActualizarRepositorio<UsuarioEntity>, UsuarioRepositorio>();
builder.Services.AddScoped<IEliminarRepositorio, UsuarioRepositorio>();

builder.Services.AddScoped<ICrearRepositorio<AsignacionRolEntity>, AsignacionRolRepositorio>();
builder.Services.AddScoped<IActualizarRepositorio<AsignacionRolEntity>, AsignacionRolRepositorio>();
builder.Services.AddScoped<IAsignarGrupoUsuarioRepositorio, AsignacionRolRepositorio>();

builder.Services.AddScoped<IMapper<RegistroUsuarioDTO, UsuarioEntity>, RegistroDtoToUsuarioMapper>();
builder.Services.AddScoped<IMapper<RegistroUsuarioDTO, AsignacionRolEntity>, RegistroDtoToAsignacionRolMapper>();
builder.Services.AddScoped<IMapper<UsuarioEntity, PerfilUsuarioDTO>, UsuarioEntityToPerfilDtoMapper>();

builder.Services.AddScoped<ICrearUseCase<RegistroUsuarioDTO>, RegistrarUsuarioUseCase>();
builder.Services.AddScoped<LoginUsuarioUseCase>();
//builder.Services.AddScoped<ObtenerPerfilUsuarioUseCase>();
//builder.Services.AddScoped<IActualizarUseCase<PerfilUsuarioDTO>, ActualizarPerfilUsuarioUseCase>();
//builder.Services.AddScoped<IEliminarUseCase, EliminarUsuarioUseCase>();

// ===============================================
// Servicios de Grupos
// ===============================================
builder.Services.AddScoped<ILeerRepositorio<GrupoEntity>, GrupoRepositorio>();
builder.Services.AddScoped<ICrearRepositorio<GrupoEntity>, GrupoRepositorio>();
builder.Services.AddScoped<ILeerUseCase<GrupoDTO>, ObtenerGruposUseCase>();
builder.Services.AddScoped<ICrearUseCase<RegistroGrupoDTO>, RegistrarGrupoUseCase>();

// ===============================================
// Servicios de Protagonistas
// ===============================================
// Repositorios
builder.Services.AddScoped<ICrearRepositorio<PersonaEntity>, PersonaRepositorio>();
builder.Services.AddScoped<IActualizarRepositorio<PersonaEntity>, PersonaRepositorio>();
builder.Services.AddScoped<ICrearRepositorio<ProtagonistaEntity>, ProtagonistaRepositorio>();
builder.Services.AddScoped<ILeerRepositorio<ProtagonistaEntity>, ProtagonistaRepositorio>();
builder.Services.AddScoped<IActualizarRepositorio<ProtagonistaEntity>, ProtagonistaRepositorio>();
builder.Services.AddScoped<ProtagonistaRepositorio>(); // Para usar sus métodos específicos directamente

// Mappers
builder.Services.AddScoped<IMapper<RegistroProtagonistaDTO, PersonaEntity>, RegistroDtoToPersonaMapper>();
builder.Services.AddScoped<IMapper<RegistroProtagonistaDTO, ProtagonistaEntity>, RegistroDtoToProtagonistaMapper>();
builder.Services.AddScoped<IMapper<ProtagonistaEntity, PerfilProtagonistaDTO>, ProtagonistaEntityToPerfilDtoMapper>();

// Casos de Uso
builder.Services.AddScoped<ICrearUseCase<RegistroProtagonistaDTO>, RegistrarProtagonistaUseCase>();
builder.Services.AddScoped<ILeerUseCase<PerfilProtagonistaDTO>, ObtenerPerfilProtagonistaUseCase>();
builder.Services.AddScoped<IActualizarUseCase<PerfilProtagonistaDTO>, ActualizarPerfilProtagonistaUseCase>();
builder.Services.AddScoped<EliminarProtagonistaUseCase>(); // Registrado por clase concreta
builder.Services.AddScoped<ObtenerProtagonistasPorRamaUseCase>(); // Registrado por clase concreta
builder.Services.AddScoped<IActualizarUseCase<AsignarPatrullaDTO>, AsignarPatrullaProtagonistaUseCase>();
builder.Services.AddScoped<IActualizarUseCase<CambiarRamaProtagonistaDTO>, CambiarRamaProtagonistaUseCase>();

// ===============================================
// Servicios Transaccionales (Cumplimientos e Insignias)
// ===============================================
builder.Services.AddScoped<ICrearRepositorio<CumpleEntity>, CumpleRepositorio>();
builder.Services.AddScoped<ICrearRepositorio<ObtieneEntity>, ObtieneRepositorio>();
builder.Services.AddScoped<IMapper<CumpleDTO, CumpleEntity>, CumpleDtoToEntityMapper>();
builder.Services.AddScoped<IMapper<ObtieneDTO, ObtieneEntity>, ObtieneDtoToEntityMapper>();
builder.Services.AddScoped<ICrearUseCase<CumpleDTO>, RegistrarCumplimientoAccionUseCase>();
//builder.Services.AddScoped<ICrearUseCase<ObtenerInsigniaDTO>, OtorgarInsigniaUseCase>();


// ===============================================
// Servicios de Familiares
// ===============================================
// Repositorios
builder.Services.AddScoped<ICrearRepositorio<FamiliarEntity>, FamiliarRepositorio>();
builder.Services.AddScoped<IActualizarRepositorio<FamiliarEntity>, FamiliarRepositorio>();
builder.Services.AddScoped<IEsFamiliaRepositorio, EsFamiliaRepositorio>();

// Casos de Uso
builder.Services.AddScoped<ICrearUseCase<RegistroFamiliarCompletoDTO>, RegistrarFamiliarUseCase>();
builder.Services.AddScoped<ICrearUseCase<AsignarFamiliarExistenteDTO>, AsignarFamiliarExistenteUseCase>();
builder.Services.AddScoped<ObtenerFamiliaresPorProtagonistaUseCase>();
builder.Services.AddScoped<EliminarVinculoFamiliarUseCase>();

// ===============================================
// Servicios de Competencias en Desarrollo (Desarrolla)
// ===============================================
// Repositorios
builder.Services.AddScoped<IDesarrollaRepositorio, DesarrollaRepositorio>();

// Casos de Uso
builder.Services.AddScoped<ICrearUseCase<AsignarCompetenciaDTO>, AsignarCompetenciaUseCase>();
builder.Services.AddScoped<ObtenerCompetenciasPorProtagonistaUseCase>();

// =======================================================
// 3. BASE DE DATOS
// =======================================================
builder.Services.AddDbContext<EducaramaContext>(options =>
    options.UseSqlServer(cadenaDeConexion));

var app = builder.Build();

// Area de middlewares
app.UseCors("AllowAll");
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();

app.Run();