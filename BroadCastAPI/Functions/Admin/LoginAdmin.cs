using BroadCastAPI.Models;
using BroadCastAPI.Services.DataOperations;
using BroadCastAPI.Services.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace BroadCastAPI.Functions.Admin
{
    public class LoginAdmin
    {
        private readonly ILogger<LoginAdmin> _logger;
        private readonly EventManagementContext _context;

        public LoginAdmin(ILogger<LoginAdmin> logger, EventManagementContext context)
        {
            _logger = logger;
            _context = context;
        }

        [OpenApiOperation(operationId: "AdminLogin", tags: new[] {"AdminLogin"}, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "Username", In = ParameterLocation.Query, Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("LoginAdmin")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            return await FunctionService.GetAdmin(req.Query["Username"]!, _context);
        }
    }
}
