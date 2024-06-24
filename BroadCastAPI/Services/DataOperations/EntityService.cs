using BroadCastAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadCastAPI.Services.DataOperations
{
    public class EntityService : IEntityService
    {
        private readonly EventManagementContext _context;
        private readonly ICRUDService _crudService;
        public EntityService(EventManagementContext context, ICRUDService cRUDService)
        {
            _context = context;
            _crudService = cRUDService;
        }
        public async Task<IActionResult> AddEntity<T>(T? entity) where T : class
        {
            return await _crudService.AddEntity(entity, _context);
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
