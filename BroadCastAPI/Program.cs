using BroadCastAPI.Models;
using BroadCastAPI.Services.DataOperations;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.ResponseCompression;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureOpenApi()
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        var configuration = context.Configuration;
        string connectionString = configuration.GetSection("ConnectionStrings:defaultConnection").Value!;
        services.AddDbContext<EventManagementContext>(options => options.UseSqlServer(connectionString, serverOptions =>
        {
            serverOptions.EnableRetryOnFailure();
        }));
        services.AddScoped<ICRUDService, CRUDService>();
        services.AddScoped<IEntityService, EntityService>();
    })
    .Build();

host.Run();