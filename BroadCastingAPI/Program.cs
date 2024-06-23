using BroadCastingAPI.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
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
    })
    .Build();

host.Run();
