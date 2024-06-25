using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace BroadCastAPI.Functions.Admin
{
    public class DisplayForParticipants
    {
        private readonly ILogger<DisplayForParticipants> _logger;

        public DisplayForParticipants(ILogger<DisplayForParticipants> logger)
        {
            _logger = logger;
        }

        [Function("DisplayForParticipants")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
