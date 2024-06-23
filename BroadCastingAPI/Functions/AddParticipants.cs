using BroadCastingAPI.Services.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using EntityType = BroadCastingAPI.Models.Participant;

namespace BroadCastingAPI.Functions
{
    public class AddParticipants
    {
        private readonly ILogger<AddParticipants> _logger;

        public AddParticipants(ILogger<AddParticipants> logger)
        {
            _logger = logger;
        }

        [OpenApiOperation(operationId: "AddParticipant", tags: new[] {nameof(EntityType)})]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(EntityType), Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType:typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType:typeof(string))]
        [Function("AddParticipants")]

        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "RegiterParticipants")] HttpRequestData req)
        {
            return new OkObjectResult("Heelo");
        }
    }
}
