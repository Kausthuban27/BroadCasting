using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BroadCastAPI.Services.DataOperations
{
    public interface IEntityService
    { 
        public Task<List<T>> GetContent<T>() where T : class;
        public Task<List<T>> GetEntity<T>(NameValueCollection param) where T : class;
        public Task<IActionResult> AddEntity<T>(T? entity) where T : class;
        public Task SaveEntity<T>(T entity) where T : class;
    }
}
