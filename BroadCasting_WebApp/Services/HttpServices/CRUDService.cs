using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Http;

namespace BroadCasting_WebApp.Services.HttpServices
{
    public class CRUDService : ICRUDService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CRUDService> _logger;
        public CRUDService(IHttpClientFactory factory, ILogger<CRUDService> logger)
        {
            _httpClient = factory.CreateClient("DataApi");
            _logger = logger;
        }

        public async Task<IActionResult> AddParticipant<T>(Uri basePath, T entity) where T : class
        {
            try
            { 
                var jsonString = JsonConvert.SerializeObject(entity, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });

                var req = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = basePath,
                    Content = new StringContent(jsonString, Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(req);
                if(response.IsSuccessStatusCode)
                {
                    return new OkObjectResult("");
                }
                return new BadRequestObjectResult("");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception Occurred {ex}");
                return new InternalServerErrorResult();
            }
        }

        public async Task<IActionResult> GetAdmin(Uri basePath, string username)
        {
            try
            {
                UriBuilder uriBuilder = new UriBuilder(basePath);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["Username"] = username;
                uriBuilder.Query = query.ToString();

                await _httpClient.GetAsync(uriBuilder.Uri);
                return new OkObjectResult("Admin Exists");
            }
            catch (Exception)
            {
                return new InternalServerErrorResult();
            }
        }

        public async Task<IActionResult> GetParticipant(Uri basePath, string email, string designation)
        {
            try
            {
                UriBuilder uriBuilder = new UriBuilder(basePath);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["Email"] = email;
                query["Designation"] = designation;

                uriBuilder.Query = query.ToString();

                await _httpClient.GetAsync(uriBuilder.Uri);
                return new OkObjectResult("Participant Exists");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception Occurred {ex}");
                return new InternalServerErrorResult();
            }
        }
    }
}
