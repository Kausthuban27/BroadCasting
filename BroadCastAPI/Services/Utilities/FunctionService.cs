using BroadCastAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BroadCastAPI.Services.Utilities
{
    public static class FunctionService
    {
        public static readonly List<Type> PostBadRequestException = new()
        {
            typeof(ArgumentNullException),
            typeof(DbException),
            typeof(JsonException),
            typeof(InvalidDataException)
        };

        public static readonly List<Type> PostLoggedException = new();

        public static readonly List<Type> GetBadRequestException = new()
        {
            typeof(ArgumentException),
            typeof(ArgumentNullException),
            typeof(DbException),
            typeof(MissingMemberException)
        };

        public static readonly List<Type> GetLoggedException = new()
        {
            typeof(ArgumentNullException),
            typeof(DbException),
            typeof(InvalidDataException)
        };
        private static bool ContainsException(List<Type> types, Exception ex) => types.Contains(ex.GetType()) || ex.GetType().BaseType != null && types.Contains(ex.GetType().BaseType!);
        
        public static async Task<HttpResponseData> PostResponse<T>(
            HttpRequestData req,
            Func<T?, Task> func,
            ILogger _logger,
            List<Type>? additionalLoggedExceptions = null,
            List<Type>? additionalBadRequestException = null) where T : class
        {
            var logged = additionalLoggedExceptions ?? new List<Type>();
            logged.AddRange(PostLoggedException);
            var badRequest = additionalBadRequestException ?? new List<Type>();
            badRequest.AddRange(PostBadRequestException);
            var res = req.CreateResponse();
            try
            {
                string requestBodyString = await new StreamReader(req.Body).ReadToEndAsync();
                T? requestBody = JsonConvert.DeserializeObject<T>(requestBodyString);
                await func(requestBody);
                res.StatusCode = HttpStatusCode.OK;
                return res;
            }
            catch (Exception ex) when (ContainsException(logged, ex))
            {
                return await HandleErrorResponse(res, ex, badRequest);
            }
            catch (Exception ex)
            {
                return await HandleErrorResponse(res, ex, badRequest, _logger);
            }
        }
        
        public static async Task<HttpResponseData> GetResponse<T>(
            HttpRequestData req, 
            Func<Task<T?>> func,
            ILogger _logger, 
            List<Type>? additionalLoggedException = null,
            List<Type>? additionalBadRequestException = null) where T : class
        {
            var logged = additionalLoggedException ?? new List<Type>();
            logged.AddRange(GetLoggedException);
            var badRequest = additionalBadRequestException ?? new List<Type>();
            badRequest.AddRange(GetBadRequestException);

            var res = req.CreateResponse();
            try
            {
                var jsonString = JsonConvert.SerializeObject(await func(), new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
                await res.WriteAsJsonAsync(jsonString);
                res.StatusCode = HttpStatusCode.OK;
                res.Headers.Add("Content-Type", "application/json");
                return res;
            }
            catch(Exception ex) when (ContainsException(logged, ex))
            {
                return await HandleErrorResponse(res, ex, badRequest);
            }
            catch(Exception ex)
            {
                return await HandleErrorResponse(res, ex, badRequest, _logger);
            }
        }

        public static async Task<IActionResult> GetAdmin(string username, EventManagementContext context)
        {
            var adminExists = await context.Admins.Where(u => u.Name == username).FirstOrDefaultAsync();
            if (adminExists == null)
            {
                throw new MissingMemberException();
            }
            return new OkObjectResult("Admin Exisits");
        }

        public static async Task<IActionResult> GetParticipant(string email, string designation, EventManagementContext context)
        {
            var participantExists = await context.Participants.Where(e => e.ParticipantEmail == email && e.Designation == designation).FirstOrDefaultAsync();
            if (participantExists == null)
            {
                throw new MissingMemberException();
            }
            return new OkObjectResult("Participant Exisits");
        }

        public static async Task<HttpResponseData> HandleErrorResponse(HttpResponseData httpResponseData, Exception ex, List<Type> badRequestExceptions, ILogger? logger = null)
        {
            logger?.LogError(ex, $"Exception occurred during function execution.");
            if (ContainsException(badRequestExceptions, ex))
            {
                httpResponseData.Headers.Add("Content-Type", "text/plain");
                httpResponseData.StatusCode = HttpStatusCode.BadRequest;
                await httpResponseData.WriteStringAsync(ex.Message ?? "Bad Request");
                return httpResponseData;
            }
            httpResponseData.StatusCode = HttpStatusCode.InternalServerError;
            return httpResponseData;
        }
    }
}
