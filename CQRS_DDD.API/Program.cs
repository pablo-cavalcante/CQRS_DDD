#pragma warning disable
using CQRS_DDD.Application.Commands.Fruta;
using CQRS_DDD.Application.Services;
using CQRS_DDD.Domain.Interfaces;
using CQRS_DDD.Infrastructure.Persistence;
using CQRS_DDD.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Teste Project", Version = "v1" });
});

builder.Services.AddDbContext<PgsqlContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IFrutaRepository, FrutaRepository>();
builder.Services.AddScoped<ICiLojaRepository, CiLojaRepository>();

#region FrutaCommands

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateFrutaCommand).Assembly));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(UpdateFrutaCommand).Assembly));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(DeleteFrutaCommand).Assembly));

#endregion

#region ComunicadoLojaCommands

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateComunicadoLojaCommand).Assembly));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(UpdateComunicadoLojaCommand).Assembly));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(DeleteComunicadoLojaCommand).Assembly));

#endregion

builder.Services.AddHostedService<RabbitMqConsumerService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PgsqlContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Teste Project");
    //c.RoutePrefix = "";
});

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapSwagger();
});

app.Run();

public partial class Program { } // <- necessário para testes de integração