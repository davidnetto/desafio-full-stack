using CleanCar.Application;
using CleanCar.Domain;
using CleanCar.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);


// Register Configuration
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


//builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Add Database Service

builder.Services.AddDbContext<VeiculoDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("CleanCar.API")));

builder.Services.AddDbContext<LocadoraDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("CleanCar.API")));

builder.Services.AddDbContext<MontadoraDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("CleanCar.API")));

builder.Services.AddDbContext<ModeloDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("CleanCar.API")));


builder.Services.AddDbContext<EnderecoDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("CleanCar.API")));

builder.Services.AddDbContext<LogDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("CleanCar.API")));

builder.Services.AddScoped<IMontadoraService, MontadoraService>();
builder.Services.AddScoped<IMontadoraRepository, MontadoraRepository>();



builder.Services.AddScoped<IVeiculoService, VeiculoService>();
builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();

builder.Services.AddScoped<ILocadoraService, LocadoraService>();
builder.Services.AddScoped<ILocadoraRepository, LocadoraRepository>();

builder.Services.AddScoped<IModeloService, ModeloService>();
builder.Services.AddScoped<IModeloRepository, ModeloRepository>();

builder.Services.AddScoped<IEnderecoService, EnderecoService>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();

builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<ILogRepository, LogRepository>();

var app = builder.Build();
app.UseCors("DevelopmentPolicy");

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
