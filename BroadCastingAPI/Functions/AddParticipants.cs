using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BroadCastingAPI.Functions
{
    public class AddParticipants
    {
        private readonly ILogger<AddParticipants> _logger;

        public AddParticipants(ILogger<AddParticipants> logger)
        {
            _logger = logger;
        }

        [Function("AddParticipants")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult(HttpStatusCode.OK);
        }
    }
}
