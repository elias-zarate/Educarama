using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Acciones.CasosDeUso;
using Aplicacion.Acciones.DTOs;
using Aplicacion.Acciones.Mappers;
using Aplicacion.Avisos.CasosDeUso;
using Aplicacion.Avisos.DTOs;
using Aplicacion.Avisos.Mappers;
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

// Configuración única de CORS para permitir peticiones del Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var cadenaDeConexion = builder.Configuration.GetConnectionString("DefaultConnection");

// ===============================================
// Base de Datos
// ===============================================
builder.Services.AddDbContext<EducaramaContext>(options =>
    options.UseSqlServer(cadenaDeConexion));

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
builder.Services.AddScoped<ICrearRepositorio<RamaEntity>, RamaRepositorio>();
builder.Services.AddScoped<ILeerRamaPorCodigoRepositorio, RamaRepositorio>();
builder.Services.AddScoped<ILeerAsignacionPorUsuarioRepositorio, AsignacionRolRepositorio>();
builder.Services.AddScoped<UnirseRamaUseCase>();

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
builder.Services.AddScoped<ObtenerPatrullasPorContextoUseCase>();

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
builder.Services.AddScoped<ObtenerPerfilUsuarioUseCase>();
builder.Services.AddScoped<IActualizarUseCase<PerfilUsuarioDTO>, ActualizarPerfilUsuarioUseCase>();
builder.Services.AddScoped<IEliminarUseCase, EliminarUsuarioUseCase>();
builder.Services.AddScoped<ObtenerPerfilPorEmailUseCase>();
builder.Services.AddScoped<UsuarioEntityToPerfilDtoMapper>();
builder.Services.AddScoped<ObtenerContextoUsuarioUseCase>();
builder.Services.AddScoped<CerrarSesionUseCase>();

// ===============================================
// Servicios de Grupos
// ===============================================
builder.Services.AddScoped<ILeerRepositorio<GrupoEntity>, GrupoRepositorio>();
builder.Services.AddScoped<ICrearRepositorio<GrupoEntity>, GrupoRepositorio>();
builder.Services.AddScoped<ILeerUseCase<GrupoDTO>, ObtenerGruposUseCase>();
builder.Services.AddScoped<ICrearUseCase<RegistroGrupoDTO>, RegistrarGrupoUseCase>();
builder.Services.AddScoped<UnirseGrupoUseCase>();
builder.Services.AddScoped<ILeerGrupoPorCodigoRepositorio, GrupoRepositorio>();

// ===============================================
// Servicios de Protagonistas
// ===============================================
builder.Services.AddScoped<ICrearRepositorio<PersonaEntity>, PersonaRepositorio>();
builder.Services.AddScoped<IActualizarRepositorio<PersonaEntity>, PersonaRepositorio>();
builder.Services.AddScoped<ICrearRepositorio<ProtagonistaEntity>, ProtagonistaRepositorio>();
builder.Services.AddScoped<ILeerRepositorio<ProtagonistaEntity>, ProtagonistaRepositorio>();
builder.Services.AddScoped<IActualizarRepositorio<ProtagonistaEntity>, ProtagonistaRepositorio>();
builder.Services.AddScoped<ProtagonistaRepositorio>();
builder.Services.AddScoped<ILeerProtagonistasPorRamaRepositorio, ProtagonistaRepositorio>();

builder.Services.AddScoped<IMapper<RegistroProtagonistaDTO, PersonaEntity>, RegistroDtoToPersonaMapper>();
builder.Services.AddScoped<IMapper<RegistroProtagonistaDTO, ProtagonistaEntity>, RegistroDtoToProtagonistaMapper>();
builder.Services.AddScoped<IMapper<ProtagonistaEntity, PerfilProtagonistaDTO>, ProtagonistaEntityToPerfilDtoMapper>();

builder.Services.AddScoped<ICrearUseCase<RegistroProtagonistaDTO>, RegistrarProtagonistaUseCase>();
builder.Services.AddScoped<ILeerUseCase<PerfilProtagonistaDTO>, ObtenerPerfilProtagonistaUseCase>();
builder.Services.AddScoped<IActualizarUseCase<PerfilProtagonistaDTO>, ActualizarPerfilProtagonistaUseCase>();
builder.Services.AddScoped<EliminarProtagonistaUseCase>();
builder.Services.AddScoped<ObtenerProtagonistasPorRamaUseCase>();
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
// Descomentar cuando implementes el caso de uso de insignias:
// builder.Services.AddScoped<ICrearUseCase<ObtenerInsigniaDTO>, OtorgarInsigniaUseCase>();

// ===============================================
// Servicios de Familiares
// ===============================================
builder.Services.AddScoped<ICrearRepositorio<FamiliarEntity>, FamiliarRepositorio>();
builder.Services.AddScoped<IActualizarRepositorio<FamiliarEntity>, FamiliarRepositorio>();
builder.Services.AddScoped<IEsFamiliaRepositorio, EsFamiliaRepositorio>();

builder.Services.AddScoped<ICrearUseCase<RegistroFamiliarCompletoDTO>, RegistrarFamiliarUseCase>();
builder.Services.AddScoped<ICrearUseCase<AsignarFamiliarExistenteDTO>, AsignarFamiliarExistenteUseCase>();
builder.Services.AddScoped<ObtenerFamiliaresPorProtagonistaUseCase>();
builder.Services.AddScoped<EliminarVinculoFamiliarUseCase>();

// ===============================================
// Servicios de Competencias en Desarrollo (Desarrolla)
// ===============================================
builder.Services.AddScoped<IDesarrollaRepositorio, DesarrollaRepositorio>();
builder.Services.AddScoped<ICrearUseCase<AsignarCompetenciaDTO>, AsignarCompetenciaUseCase>();
builder.Services.AddScoped<ObtenerCompetenciasPorProtagonistaUseCase>();

// ===============================================
// Servicios de Avisos
// ===============================================
// Repositorios
builder.Services.AddScoped<ICrearRepositorio<AvisoEntity>, AvisoRepositorio>();
builder.Services.AddScoped<ILeerRepositorio<AvisoEntity>, AvisoRepositorio>();
builder.Services.AddScoped<IActualizarRepositorio<AvisoEntity>, AvisoRepositorio>();
builder.Services.AddScoped<IEliminarRepositorio, AvisoRepositorio>();
builder.Services.AddScoped<ILeerAvisosActivosRepositorio, AvisoRepositorio>();

// Mappers
builder.Services.AddScoped<IMapper<AvisoEntity, AvisoDTO>, AvisoEntityToDtoMapper>();
builder.Services.AddScoped<IMapper<RegistroAvisoDTO, AvisoEntity>, RegistroDtoToAvisoMapper>();

// Casos de Uso
builder.Services.AddScoped<ICrearUseCase<RegistroAvisoDTO>, RegistrarAvisoUseCase>();
builder.Services.AddScoped<ObtenerAvisosUseCase>();
builder.Services.AddScoped<ObtenerAvisosActivosUseCase>();

var app = builder.Build();

// ===============================================
// Área de Middlewares
// ===============================================
app.UseCors("PermitirFrontend"); // Siempre antes de MapControllers

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();

app.Run();