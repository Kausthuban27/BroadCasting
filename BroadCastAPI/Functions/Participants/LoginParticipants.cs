#nullable disable
using BroadCastAPI.Models;
using BroadCastAPI.Services.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace BroadCastAPI.Functions.Participants
{
    public class LoginParticipants
    {
        private readonly ILogger<LoginParticipants> _logger;
        private readonly EventManagementContext _context;
        public LoginParticipants(ILogger<LoginParticipants> logger, EventManagementContext context)
        {
            _logger = logger;
            _context = context;
        }

        [OpenApiOperation(operationId: "LoginParticipant", tags: new[] {""})]
        [OpenApiParameter(name: "Email", In = ParameterLocation.Query, Required = true)]
        [OpenApiParameter(name: "Designation", In = ParameterLocation.Query, Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("LoginParticipants")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            return await FunctionService.GetParticipant(req.Query["Email"], req.Query["Designation"], _context);
        }
    }
}
