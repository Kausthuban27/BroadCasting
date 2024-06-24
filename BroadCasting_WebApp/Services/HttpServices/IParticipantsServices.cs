using Microsoft.AspNetCore.Mvc;

namespace BroadCasting_WebApp.Services.HttpServices
{
    public interface IParticipantsServices
    {
        public Task<IActionResult> AddParticipants<T>(T entity) where T : class;
    }
}
