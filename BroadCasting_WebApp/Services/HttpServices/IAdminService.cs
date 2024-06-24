using Microsoft.AspNetCore.Mvc;

namespace BroadCasting_WebApp.Services.HttpServices
{
    public interface IAdminService
    {
        public Task<IActionResult> AdminLogin(string username);
    }
}
