using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<T>> GetUpdatedEventContent<T>(DbContext context) where T : class
        {
            var dbSet = context.Set<T>();
            return await dbSet.AsNoTracking().ToListAsync();
        }
        public async Task<List<T>> GetEventContent<T>(DbContext context) where T : class
        {
            try
            {
                var dbSet = context.Set<T>();
                return await dbSet.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occured {ex}");
                return new List<T> { };
            }
        }

        public Task SaveEntity<T>(T entity, DbContext context)
        {
            throw new NotImplementedException();
        }
    }
}
