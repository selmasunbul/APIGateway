using Core.Abstract;
using Core.Helpers;
using System.ComponentModel.Design;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")  // appsettings.json dosyanýzýn adýný ve yolunu ayarlayýn
    .Build();


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IMessageService, MessageService>();






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
