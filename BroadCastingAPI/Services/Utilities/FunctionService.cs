using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BroadCastingAPI.Services.Utilities
{
    public class FunctionService
    {
        public static readonly List<Type> PostBadRequestException = new()
        {
            typeof(ArgumentNullException),
            typeof(DbException),
            typeof(JsonException),
            typeof(InvalidDataException)
        };

        public static readonly List<Type> PostLoggedException = new()
        {
            typeof(ArgumentNullException),
            typeof(DbException),
            typeof(InvalidDataException)
        };

        private static bool ContainsException(List<Type> types, Exception ex) => types.Contains(ex.GetType()) || ex.GetType().BaseType != null && types.Contains(ex.GetType().BaseType!);
        
        public static async Task<HttpResponseData> PostResponse<T>(
            HttpRequestData req,
            Func<List<T>?, Task> func,
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
                List<T>? requestBody = DeserializeService<List<T>>.DeserializeStream(req.Body);
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
