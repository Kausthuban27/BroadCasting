using BroadCastAPI.Services.DataOperations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using System.Net;
using EntityType = BroadCastAPI.Models.EventContent;

namespace BroadCastAPI.Functions.Admin
{
    public class BroadcastToParticipants
    {
        private readonly ILogger<BroadcastToParticipants> _logger;
        private readonly IEntityService _entityService;

        public BroadcastToParticipants(ILogger<BroadcastToParticipants> logger, IEntityService entityService)
        {
            _logger = logger;
            _entityService = entityService;
        }

        [OpenApiOperation(operationId: "BroadcastToParticipants", tags: new[] {""})]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(EntityType))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("BroadcastToParticipants")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            var res = req.CreateResponse();
            var data = await _entityService.GetContent<EntityType>();
            await res.WriteAsJsonAsync(data);

            return res;
        }
    }
}
