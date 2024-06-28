using BroadCastAPI;
using BroadCasting_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;

namespace BroadCasting_WebApp.Services.HttpServices
{
    public class ParticipantService : IParticipantsServices
    {
        private readonly BroadCastAPIConfig _config;
        private readonly ICRUDService _crudService;
        private readonly Uri _baseUrl, _loginParticipantUrl, _broadcastUrl;
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
            _loginParticipantUrl = new(baseUrl, RouteConstants.LoginParticipant);
            _broadcastUrl = new(baseUrl, RouteConstants.BroadcastToParticipants);
        }
        public async Task<IActionResult> AddParticipants<T>(T entity) where T : class
        {
            return await _crudService.AddParticipant<T>(_baseUrl, entity);
        }

        public async Task<IActionResult> GetParticipants(string email, string designation)
        {
            return await _crudService.GetParticipant(_loginParticipantUrl, email, designation);
        }

        public async Task<List<T>> GetEventContent<T>() where T : class
        {
            return await _crudService.GetContent<T>(_broadcastUrl);
        }
    }
}
