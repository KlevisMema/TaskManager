using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManagment.SECURITY.ApplicationAuthorizationService.ServiceInterface;

namespace TaskManagment.SECURITY.ApplicationAuthorizationService.ServiceImplementation
{
    public class ApiKeyAuthorizationFilter : IAuthorizationFilter, IApiKeyAuthorizationFilter
    {
        private readonly string _apiKey;

        public ApiKeyAuthorizationFilter
        (
           string apiKey
        )
        {
            _apiKey = apiKey;
        }

        public void
        OnAuthorization
        (
            AuthorizationFilterContext context
        )
        {
            string? apiKey = context.HttpContext.Request.Headers["X-Api-Key"].FirstOrDefault();

            if (String.IsNullOrEmpty(apiKey))
                context.Result = new UnauthorizedResult();

            if (apiKey != _apiKey)
                context.Result = new UnauthorizedResult();
        }
    }
}