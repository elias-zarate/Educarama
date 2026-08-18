using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.CategoriaRamas.CasosDeUso;
using Aplicacion.CategoriaRamas.DTOs;
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
using Repositorio; // <-- Necesario para inyectar los repositorios

var builder = WebApplication.CreateBuilder(args);

// Area de servicios
builder.Services.AddControllers();

var cadenaDeConexion = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddScoped<ILeerRepositorio<CategoriaRamaEntity>, CategoriaRamaRepositorio>();

builder.Services.AddScoped<ILeerRepositorio<RamaEntity>, RamaRepositorio>();
builder.Services.AddScoped<ICrearRepositorio<RamaEntity>, RamaRepositorio>();

builder.Services.AddScoped<ILeerRepositorio<PatrullaEntity>, PatrullaRepositorio>();
builder.Services.AddScoped<ILeerPatrullasPorRamaRepositorio, PatrullaRepositorio>();


// Servicios de Usuario
builder.Services.AddScoped<ICrearUseCase<RegistroUsuarioDTO>, RegistrarUsuarioUseCase>();
builder.Services.AddScoped<ObtenerPerfilUsuarioUseCase>();
builder.Services.AddScoped<IActualizarUseCase<PerfilUsuarioDTO>, ActualizarPerfilUsuarioUseCase>();
builder.Services.AddScoped<IEliminarUseCase, EliminarUsuarioUseCase>();

// Servicios de Categoría Rama
builder.Services.AddScoped<ILeerUseCase<CategoriaRamaDTO>, ObtenerCategoriaRamasUseCase>();

// Servicios de Rama
builder.Services.AddScoped<IMapper<RegistroRamaDTO, RamaEntity>, RegistroRamaDtoToEntityMapper>();
builder.Services.AddScoped<ILeerUseCase<RamaDTO>, ObtenerRamasUseCase>();
builder.Services.AddScoped<ICrearUseCase<RegistroRamaDTO>, CrearRamaUseCase>();

// Servicios de Patrulla
builder.Services.AddScoped<IMapper<PatrullaEntity, PatrullaDTO>, PatrullaEntityToDtoMapper>();
builder.Services.AddScoped<ILeerUseCase<PatrullaDTO>, ObtenerPatrullasUseCase>();
builder.Services.AddScoped<ILeerPatrullasPorRamaUseCase, ObtenerPatrullasPorRamaUseCase>();

// Mappers 
builder.Services.AddScoped<IMapper<RegistroProtagonistaDTO, PersonaEntity>, RegistroDtoToPersonaMapper>();
builder.Services.AddScoped<IMapper<RegistroProtagonistaDTO, ProtagonistaEntity>, RegistroDtoToProtagonistaMapper>();
builder.Services.AddScoped<IMapper<ProtagonistaEntity, PerfilProtagonistaDTO>, ProtagonistaEntityToPerfilDtoMapper>();

// Casos de Uso Generales
builder.Services.AddScoped<ICrearUseCase<RegistroProtagonistaDTO>, RegistrarProtagonistaUseCase>();
builder.Services.AddScoped<ILeerUseCase<PerfilProtagonistaDTO>, ObtenerPerfilProtagonistaUseCase>();
builder.Services.AddScoped<IActualizarUseCase<PerfilProtagonistaDTO>, ActualizarPerfilProtagonistaUseCase>();
builder.Services.AddScoped<IEliminarUseCase, EliminarProtagonistaUseCase>();

// Casos de Uso Específicos
builder.Services.AddScoped<ObtenerProtagonistasPorRamaUseCase>();
builder.Services.AddScoped<IActualizarUseCase<AsignarPatrullaDTO>, AsignarPatrullaProtagonistaUseCase>();
builder.Services.AddScoped<IActualizarUseCase<CambiarRamaProtagonistaDTO>, CambiarRamaProtagonistaUseCase>();

// Repositorios (por si no los agregaste antes)
builder.Services.AddScoped<ICrearRepositorio<CumpleEntity>, CumpleRepositorio>();
builder.Services.AddScoped<ICrearRepositorio<ObtieneEntity>, ObtieneRepositorio>();

// Mappers
builder.Services.AddScoped<IMapper<CumpleDTO, CumpleEntity>, CumpleDtoToEntityMapper>();
builder.Services.AddScoped<IMapper<ObtieneDTO, ObtieneEntity>, ObtieneDtoToEntityMapper>();

// Casos de Uso
builder.Services.AddScoped<ICrearUseCase<CumpleDTO>, RegistrarCumplimientoAccionUseCase>();
builder.Services.AddScoped<ICrearUseCase<ObtenerInsigniaDTO>, OtorgarInsigniaUseCase>();

// =======================================================
// 3. BASE DE DATOS
// =======================================================
builder.Services.AddDbContext<EducaramaContext>(options =>
    options.UseSqlServer(cadenaDeConexion));


var app = builder.Build();

// Area de middlewares
app.MapControllers();

app.Run();