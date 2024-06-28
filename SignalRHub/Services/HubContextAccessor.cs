using Microsoft.AspNetCore.SignalR;

namespace SignalRHub.Services
{
    public class HubContextAccessor : IHubContextAccessor
    {
        public IHubContext<EventHub> HubContext { get; set; }
    }
}
