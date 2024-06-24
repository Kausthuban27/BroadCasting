using BroadCastAPI.Services.DataOperations;
using BroadCastAPI.Services.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using System.Net;
using EntityType = BroadCastAPI.Models.Participant;

namespace BroadCastAPI.Functions.Participants
{
    public class RegisterParticipants
    {
        private readonly ILogger<RegisterParticipants> _logger;
        private readonly IEntityService _entityService;

        public RegisterParticipants(ILogger<RegisterParticipants> logger, IEntityService entityService)
        {
            _logger = logger;
            _entityService = entityService;
        }

        [OpenApiOperation(operationId: "RegisterParticipant", tags: new[] {""})]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(EntityType), Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("RegisterParticipants")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            return await FunctionService.PostResponse<EntityType>(req, async (entity) => await _entityService.AddEntity<EntityType>(entity), _logger);
        }
    }
}
