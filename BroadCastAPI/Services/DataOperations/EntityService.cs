#nullable disable
using BroadCastAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using TableDependency.SqlClient.Base.EventArgs;
using TableDependency.SqlClient;
using Microsoft.AspNetCore.SignalR;
using SignalRHub;

namespace BroadCastAPI.Services.DataOperations
{
    public class EntityService : IEntityService
    {
        private readonly ICRUDService _crudService;
        private readonly EventManagementContext _context;
        private readonly SqlTableDependency<EventContent> _tableDependency;
        private readonly IHubContext<EventHub> _hubContext;
        private readonly string _connectionString;

        public EntityService(ICRUDService cRUDService, IHubContext<EventHub> hubContext, EventManagementContext context)
        {
            _crudService = cRUDService;
            _context = context;
            _hubContext = hubContext;
            _connectionString = "Data Source=CEI2103\\SQLEXPRESS;Initial Catalog=EventManagement;Trusted_Connection=True;";
            _tableDependency = new SqlTableDependency<EventContent>(_connectionString, "EventContent");
            _tableDependency.OnChanged += Changed;
            _tableDependency.Start();

            Console.WriteLine(_hubContext == null ? "HubContextAccessor is null" : "HubContextAccessor is initialized");
        }

        private void Changed(object sender, RecordChangedEventArgs<EventContent> e)
        {
            try
            {
                if (_hubContext == null)
                {
                    Console.WriteLine("HubContext is null");
                    return;
                }

                var content = _crudService.GetUpdatedEventContent<EventContent>(_context);
                _hubContext.Clients.All.SendAsync("RefreshContent", content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Changed method: {ex.Message}");
            }
        }

        public async Task<IActionResult> AddEntity<T>(T entity) where T : class
        {
            return await _crudService.AddEntity(entity, _context);
        }

        public async Task<List<T>> GetContent<T>() where T : class
        {
            return await _crudService.GetEventContent<T>(_context);
        }

        public Task<List<T>> GetEntity<T>(NameValueCollection param) where T : class
        {
            throw new NotImplementedException();
        }

        public Task SaveEntity<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
