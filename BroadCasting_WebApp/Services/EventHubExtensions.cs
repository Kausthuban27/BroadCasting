using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SignalRHub.Services;
using SignalRHub;

namespace BroadCasting_WebApp.Services
{
    public static class EventHubExtensions
    {
        public static void UseEventHubContext(this IApplicationBuilder app)
        {
            var hubContextAccessor = app.ApplicationServices.GetRequiredService<IHubContextAccessor>();
            var hubContext = app.ApplicationServices.GetRequiredService<IHubContext<EventHub>>();
            hubContextAccessor.HubContext = hubContext;

            if (hubContextAccessor.HubContext == null)
            {
                Console.WriteLine("HubContext is null after setting in UseEventHubContext");
            }
            else
            {
                Console.WriteLine("HubContext is initialized in UseEventHubContext");
            }
        }
    }
}
