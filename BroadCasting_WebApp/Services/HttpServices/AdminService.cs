using BroadCastAPI;
using BroadCasting_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BroadCasting_WebApp.Services.HttpServices
{
    public class AdminService : IAdminService
    {
        private readonly BroadCastAPIConfig _config;
        private readonly ICRUDService _crudService;
        private readonly Uri _baseUrl;
        public AdminService(IOptions<BroadCastAPIConfig> config, ICRUDService service)
        {
            _config = config.Value;
            _crudService = service;
            if (_config.BaseUrl == null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            var baseUrl = new Uri(_config.BaseUrl, UriKind.Absolute);
            _baseUrl = new(baseUrl, RouteConstants.Admin);
        }
        public async Task<IActionResult> AdminLogin(string username)
        {
            return await _crudService.GetAdmin(_baseUrl, username);
        }
    }
}
