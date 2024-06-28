using Microsoft.AspNetCore.SignalR;

namespace SignalRHub.Services
{
    public interface IHubContextAccessor
    {
        public IHubContext<EventHub> HubContext { get; set; }
    }
}
