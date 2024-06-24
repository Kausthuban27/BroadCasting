#nullable disable
using BroadCastAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadCastAPI.Services.DataOperations
{
    public class CRUDService : ICRUDService
    {
        public async Task<IActionResult> AddEntity<T>(T entity, DbContext context) where T : class
        {
            var addEntity = context.Add<T>(entity);
            if (addEntity != null)
            {
                await context.SaveChangesAsync();
                return new OkObjectResult("Registration Successfull");
            }
            else
            {
                return new BadRequestObjectResult("Unsuccessfull Transaction");
            }
        }

        public Task<List<T>> GetEntity<T>(T entity, DbContext context) where T : class
        {
            throw new NotImplementedException();
        }

        public Task SaveEntity<T>(T entity, DbContext context)
        {
            throw new NotImplementedException();
        }
    }
}
