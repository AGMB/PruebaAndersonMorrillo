
using BancoPichincha.Repository;
using BancoPichincha.API.Middleware;
using BancoPichincha.Services;
using System.Reflection;
using BancoPichincha.Core.AppSettings;

var builder = WebApplication.CreateBuilder(args);
var configurationBuilder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables();
// Add services to the container.

builder.WebHost.UseUrls("http://*:5050");
builder.Services.AddOptions();
builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( s =>
{
    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Banco Pichincha Test",
        Description = "Api para ingreso a Banco Pichincha",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Email = "amorrison094@hotmail.com",
            Name = "Anderson Gustavo Morrillo Bravo"
        },
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath);
});

builder.Services.AddRepositoryServices(builder.Configuration);
builder.Services.AddServicesProjectServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.ConfigureExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
