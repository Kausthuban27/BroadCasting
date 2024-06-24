using BroadCasting_WebApp.Services.HttpServices;

namespace BroadCasting_WebApp.Services
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddHttpServices(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<ICRUDService, CRUDService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IParticipantsServices, ParticipantService>();
            return services;
        }
    }
}
