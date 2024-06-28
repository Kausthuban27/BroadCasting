using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BroadCastAPI.Services.DataOperations
{
    public interface ICRUDService
    {
        Task<List<T>> GetUpdatedEventContent<T>(DbContext context) where T : class;
        public Task<List<T>> GetEventContent<T>(DbContext context) where T : class;
        public Task<List<T>> GetEntity<T>(T entity, DbContext context) where T : class;
        public Task<IActionResult> AddEntity<T>(T? entity, DbContext context) where T : class;
        public Task SaveEntity<T>(T entity, DbContext context);
    }
}
