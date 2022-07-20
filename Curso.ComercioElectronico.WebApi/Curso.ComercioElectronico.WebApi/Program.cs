
using Curso.ComercioElectronico.Aplicacion;
using Curso.ComercioElectronico.Aplicacion.Dependency_Injection;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.ComercioElectronico.Aplicacion.ServicesImpl;

using Curso.ComercioElectronico.Infraestructura;
using Curso.ComercioElectronico.Infraestructura.Dependency_Injection;
using Curso.ComercioElectronico.Infraestructura.Repositories;
using Curso.ComercioElectronico.WebApi.Controllers;
using Curso.ComercioElectronico.WebApi.Dependency_Injection;
using Curso.CursoElectronico.Dominio.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//INYECTAR O CONFIGURAR LAS DEPENDENCIAS

///CONFIGURACION JWT PARA AUTENTIFICACION
//1. Configurar el esquema de Autentificacion JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

//Configuracion
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JWT"));

//1. Configuracion de politicas
builder.Services.AddAuthorization(options =>
{
    //1. politicas para verificar claims
    options.AddPolicy("EsEcuatoriano", policy => policy.RequireClaim("Ecuatoriano", false.ToString()));
    options.AddPolicy("TieneUnaLicencia", policy => policy.RequireClaim("TieneLicencia"));

}) ;

///usamos para ADDCORSE
builder.Services.AddCors();
builder.Services.AddInfraestructura(builder.Configuration);
builder.Services.AddAplicacion(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//2. Registra el middleware que usa los esquemas de autenticacion registrados
// OJO... el middleware autentificacion debe estar antes de cualquier compnente que requiere autentificacion

app.UseAuthentication();
app.UseAuthorization();

// Politica global CORS Middleware
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // Permitir cualquier origen
    .AllowCredentials());
app.MapControllers();
app.Run();