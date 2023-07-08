using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskManagment.SECURITY.ApplicationAuthorizationService.ServiceInterface
{
    public interface IApiKeyAuthorizationFilter
    {
        void OnAuthorization(AuthorizationFilterContext context);
    }
}