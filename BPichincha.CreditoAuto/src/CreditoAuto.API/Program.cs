using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Mapper;
using CreditoAuto.Infraestructure.Services;
using CreditoAuto.Repository;
using CreditoAuto.Repository.Context;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
TypeAdapterConfig configMapper = MapperConfig.ConfigurarMapper();
builder.Services.AddSingleton(configMapper);
builder.Services.AddScoped<IMapper, ServiceMapper>();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddDbContext<CreditoAutoDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("CreditoAutoDb");
    options.UseSqlServer(connectionString);

});
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IPatioRepository, PatioRepository>();
builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();
builder.Services.AddScoped<IAsignacionClienteRepository, AsignacionClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IPatioService, PatioService>();
builder.Services.AddScoped<IMarcaService, MarcaService>();
builder.Services.AddScoped<IAsignacionClienteService, AsignacionClienteService>();
builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddCors();
builder.Services.AddControllers()
    .AddNewtonsoftJson(opt =>
    {
        opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(builder => builder
                  .AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod());
app.UseAuthorization();

app.MapControllers();
IServiceScope scope = app.Services.CreateScope();
CreditoAutoDbContext? autoContext = scope.ServiceProvider.GetService<CreditoAutoDbContext>();
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
new InicializarDocumentos(autoContext, configuration).Inicializar();//Inicializacion para subir documentos al inico del programa
app.Run();
