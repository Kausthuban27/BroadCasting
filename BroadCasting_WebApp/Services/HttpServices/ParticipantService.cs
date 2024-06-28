using BroadCastAPI;
using BroadCastAPI.Models;
using BroadCasting_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SignalRHub;
using SignalRHub.Services;
using System.Net;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace BroadCasting_WebApp.Services.HttpServices
{
    public class ParticipantService : IParticipantsServices
    {
        private readonly BroadCastAPIConfig _config;
        private readonly ICRUDService _crudService;
        private readonly IHubContextAccessor _hubContext;
        private readonly Uri _baseUrl, _loginParticipantUrl, _broadcastUrl;
        private readonly SqlTableDependency<EventContent> _sqlTableDependency;
        private readonly string connectionString;
        public ParticipantService(IOptions<BroadCastAPIConfig> config, ICRUDService service, IHubContextAccessor hubContext)
        {
            connectionString = "Data Source=CEI2103\\SQLEXPRESS;Initial Catalog=EventManagement;Trusted_Connection=True;";
            _hubContext = hubContext;
            _sqlTableDependency = new SqlTableDependency<EventContent>(connectionString, "EventContent");
            _sqlTableDependency.OnChanged += Changed;
            _sqlTableDependency.Start();
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

        private void Changed(object sender, RecordChangedEventArgs<EventContent> e)
        {
            Task.Run(async () =>
            {
                var content = await _crudService.GetContent<EventContent>(_broadcastUrl);
                await _hubContext.HubContext.Clients.All.SendAsync("RefreshContent", content);
            }).ConfigureAwait(false);
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
