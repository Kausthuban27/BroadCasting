using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace BroadCastAPI.Functions
{
    public class GetHubContext
    {
        private readonly ILogger<GetHubContext> _logger;

        public GetHubContext(ILogger<GetHubContext> logger)
        {
            _logger = logger;
        }

        [Function("GetHubContext")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            return new OkObjectResult(true);
        }
    }
}
