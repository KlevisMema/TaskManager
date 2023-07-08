#region Usings
using Microsoft.AspNetCore.Mvc.Filters;
#endregion

namespace TaskManager.SECURITY.ApplicationAuthorizationService.ServiceInterface
{
    /// <summary>
    ///     A interface that provides security functionality for the API.
    /// </summary>
    public interface IApiKeyAuthorizationFilter
    {
        #region Method Declaration
        void 
        OnAuthorization
        (
           AuthorizationFilterContext context
        );
        #endregion
    }
}