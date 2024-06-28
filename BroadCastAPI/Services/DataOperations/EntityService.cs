#nullable disable
using BroadCastAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using TableDependency.SqlClient.Base.EventArgs;
using TableDependency.SqlClient;
using Microsoft.AspNetCore.SignalR;
using SignalRHub;
using Microsoft.Extensions.DependencyInjection;

namespace BroadCastAPI.Services.DataOperations
{
    public class EntityService : IEntityService
    {
        private readonly ICRUDService _crudService;
        private readonly EventManagementContext _context;

        public EntityService(ICRUDService cRUDService, EventManagementContext context)
        {
            _crudService = cRUDService;
            _context = context;
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
