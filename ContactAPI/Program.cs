using Business.Abstract;
using Business.Concrete;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")  // appsettings.json dosyanýzýn adýný ve yolunu ayarlayýn
    .Build();

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBContext>(options =>
             options.UseNpgsql("Host=localhost;Port=5432;Database=Rehber;User Id=postgres;Password=123456;"));

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IComminicationService, ComminicationService>();
builder.Services.AddScoped<IInfoTypeService, InfoTypeService>();
builder.Services.AddScoped<IRaportService, RaportService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("GeneralCors", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});






var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("GeneralCors");

app.Run();
