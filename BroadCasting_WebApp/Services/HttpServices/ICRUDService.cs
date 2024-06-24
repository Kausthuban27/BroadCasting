using Microsoft.AspNetCore.Mvc;

namespace BroadCasting_WebApp.Services.HttpServices
{
    public interface ICRUDService
    {
        public Task<IActionResult> GetAdmin(Uri basePath, string username);
        public Task<IActionResult> AddParticipant<T>(Uri basePath, T entity) where T : class;
    }
}
