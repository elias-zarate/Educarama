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
        policy.AllowAnyOrigin()   // Permite cualquier origen (ideal para desarrollo local con HTML/JS)
              .AllowAnyHeader()   // Permite cualquier header (Content-Type, Authorization, etc.)
              .AllowAnyMethod();  // Permite GET, POST, PUT, DELETE, PATCH, etc.
    });
});

var cadenaDeConexion = builder.Configuration.GetConnectionString("DefaultConnection");

// Repositorios de solo lectura
builder.Services.AddScoped<ILeerRepositorio<CategoriaRamaEntity>, CategoriaRamaRepositorio>();
builder.Services.AddScoped<ILeerRepositorio<RamaEntity>, RamaRepositorio>();
builder.Services.AddScoped<ILeerRepositorio<PatrullaEntity>, PatrullaRepositorio>();
builder.Services.AddScoped<ILeerPatrullasPorRamaRepositorio, PatrullaRepositorio>();
builder.Services.AddScoped<ILeerRepositorio<CompetenciaEntity>, CompetenciaRepositorio>(); // <-- Novedad
builder.Services.AddScoped<ILeerRepositorio<AccionEntity>, AccionRepositorio>(); // <-- Novedad

// Servicios de Usuario (Comentados temporalmente)
//builder.Services.AddScoped<ICrearUseCase<RegistroUsuarioDTO>, RegistrarUsuarioUseCase>();
//builder.Services.AddScoped<ObtenerPerfilUsuarioUseCase>();
//builder.Services.AddScoped<IActualizarUseCase<PerfilUsuarioDTO>, ActualizarPerfilUsuarioUseCase>();
//builder.Services.AddScoped<IEliminarUseCase, EliminarUsuarioUseCase>();

// Servicios de Categoría Rama
builder.Services.AddScoped<ILeerUseCase<CategoriaRamaDTO>, ObtenerCategoriaRamasUseCase>();
builder.Services.AddScoped<IMapper<CategoriaRamaEntity, CategoriaRamaDTO>, CategoriaRamaEntityToDtoMapper>();

// Servicios de Rama
builder.Services.AddScoped<ILeerUseCase<RamaDTO>, ObtenerRamasUseCase>();
builder.Services.AddScoped<IActualizarRepositorio<RamaEntity>, RamaRepositorio>();
builder.Services.AddScoped<GenerarCodigoAccesoRamaUseCase>();

// Servicios de Patrulla
builder.Services.AddScoped<IMapper<PatrullaEntity, PatrullaDTO>, PatrullaEntityToDtoMapper>();
builder.Services.AddScoped<ILeerUseCase<PatrullaDTO>, ObtenerPatrullasUseCase>();
builder.Services.AddScoped<ILeerPatrullasPorRamaUseCase, ObtenerPatrullasPorRamaUseCase>();

// Servicios de Competencias y Acciones (Novedad)
builder.Services.AddScoped<IMapper<CompetenciaEntity, CompetenciaDTO>, CompetenciaEntityToDtoMapper>();
builder.Services.AddScoped<IMapper<AccionEntity, AccionDTO>, AccionEntityToDtoMapper>();
builder.Services.AddScoped<ILeerUseCase<CompetenciaDTO>, ObtenerCompetenciasUseCase>();
builder.Services.AddScoped<ILeerUseCase<AccionDTO>, ObtenerAccionesUseCase>();

// Mappers de Protagonistas (Comentados temporalmente)
//builder.Services.AddScoped<IMapper<RegistroProtagonistaDTO, PersonaEntity>, RegistroDtoToPersonaMapper>();
//builder.Services.AddScoped<IMapper<RegistroProtagonistaDTO, ProtagonistaEntity>, RegistroDtoToProtagonistaMapper>();
//builder.Services.AddScoped<IMapper<ProtagonistaEntity, PerfilProtagonistaDTO>, ProtagonistaEntityToPerfilDtoMapper>();

// Casos de Uso Generales de Protagonistas (Comentados temporalmente)
//builder.Services.AddScoped<ICrearUseCase<RegistroProtagonistaDTO>, RegistrarProtagonistaUseCase>();
//builder.Services.AddScoped<ILeerUseCase<PerfilProtagonistaDTO>, ObtenerPerfilProtagonistaUseCase>();
//builder.Services.AddScoped<IActualizarUseCase<PerfilProtagonistaDTO>, ActualizarPerfilProtagonistaUseCase>();
//builder.Services.AddScoped<IEliminarUseCase, EliminarProtagonistaUseCase>();

// Casos de Uso Específicos de Protagonistas (Comentados temporalmente)
//builder.Services.AddScoped<ObtenerProtagonistasPorRamaUseCase>();
//builder.Services.AddScoped<IActualizarUseCase<AsignarPatrullaDTO>, AsignarPatrullaProtagonistaUseCase>();
//builder.Services.AddScoped<IActualizarUseCase<CambiarRamaProtagonistaDTO>, CambiarRamaProtagonistaUseCase>();

// Repositorios de transacciones
builder.Services.AddScoped<ICrearRepositorio<CumpleEntity>, CumpleRepositorio>();
builder.Services.AddScoped<ICrearRepositorio<ObtieneEntity>, ObtieneRepositorio>();

// Mappers transaccionales
builder.Services.AddScoped<IMapper<CumpleDTO, CumpleEntity>, CumpleDtoToEntityMapper>();
builder.Services.AddScoped<IMapper<ObtieneDTO, ObtieneEntity>, ObtieneDtoToEntityMapper>();

// Casos de Uso transaccionales
builder.Services.AddScoped<ICrearUseCase<CumpleDTO>, RegistrarCumplimientoAccionUseCase>();
//builder.Services.AddScoped<ICrearUseCase<ObtenerInsigniaDTO>, OtorgarInsigniaUseCase>();

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
    app.MapOpenApi(); // Exponer el documento OpenAPI (JSON)
    app.MapScalarApiReference(); // Habilitar la interfaz visual de Scalar
}

app.MapControllers();

app.Run();