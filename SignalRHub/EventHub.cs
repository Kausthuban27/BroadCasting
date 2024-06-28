using Microsoft.AspNetCore.SignalR;
using SignalRHub.Models;

namespace SignalRHub
{
    public class EventHub : Hub
    {
        public async Task RefreshContent(List<EventContent> content)
        {
            await Clients.All.SendAsync("RefreshContent", content);
        }
    }
}
