using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BroadCasting_WebApp.Services.HttpServices
{
    public interface IParticipantsServices
    {
        public Task<List<T>> GetEventContent<T>() where T : class;
        public Task<IActionResult> AddParticipants<T>(T entity) where T : class;
        public Task<IActionResult> GetParticipants(string email, string designation);
    }
}
