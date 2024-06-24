using BroadCastAPI;
using BroadCasting_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BroadCasting_WebApp.Services.HttpServices
{
    public class ParticipantService : IParticipantsServices
    {
        private readonly BroadCastAPIConfig _config;
        private readonly ICRUDService _crudService;
        private readonly Uri _baseUrl;
        public ParticipantService(IOptions<BroadCastAPIConfig> config, ICRUDService service)
        {
            _config = config.Value;
            _crudService = service;
            if (_config.BaseUrl == null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            var baseUrl = new Uri(_config.BaseUrl, UriKind.Absolute);
            _baseUrl = new(baseUrl, RouteConstants.Participant);
        }
        public async Task<IActionResult> AddParticipants<T>(T entity) where T : class
        {
            return await _crudService.AddParticipant<T>(_baseUrl, entity);
        }
    }
}
