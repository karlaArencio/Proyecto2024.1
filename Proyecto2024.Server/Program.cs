using Microsoft.EntityFrameworkCore;
using Proyecto2024.BD.Data;
using Proyecto2024.Server.Repositorio;
using System.Text.Json.Serialization;
//-------------------------------------------------------------------------------------
//configuracion de los servicios en el constructor de la aplicacion
var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers();
builder.Services.AddControllersWithViews().AddJsonOptions(
    x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(op => op.UseSqlServer("name=conn"));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IRegistroRepositorio, RegistroRepositorio>();
builder.Services.AddScoped<IDocumentoRepositorio, RegistroRepositorio>();
//--------------------------------------------------------------------------------------
//construccion de la aplicacion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
