using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BroadCasting_WebApp.Services.HttpServices
{
    public interface ICRUDService
    {
        public Task<List<T>> GetContent<T>(Uri basePath) where T : class;
        public Task<IActionResult> GetAdmin(Uri basePath, string username);
        public Task<IActionResult> AddParticipant<T>(Uri basePath, T entity) where T : class;
        public Task<IActionResult> GetParticipant(Uri basePath, string email, string designation);
    }
}
