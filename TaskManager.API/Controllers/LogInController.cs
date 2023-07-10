#region Usings
using Microsoft.AspNetCore.Mvc;
using TaskManager.DAL.DTO_s.Login;
using TaskManager.HELPERS.ServiceResponse;
using TaskManagment.USER.UserAuthorizationService.ServiceInterfaces;
#endregion

namespace TaskManager.API.Controllers
{
    /// <summary>
    ///     Log in controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LogInController : BaseController
    {
        #region Fields and Constructor
        /// <summary>
        ///     The <see cref="ILoginService"/>
        /// </summary>
        private readonly ILoginService _loginService;
        /// <summary>
        ///     The <see cref="LogInController"/> constructor
        /// </summary>
        /// <param name="loginService"> The <see cref="ILoginService"/></param>
        public LogInController
        (
           ILoginService loginService
        )
        {
            _loginService = loginService;
        }
        #endregion

        #region Endpoints

        /// <summary>
        ///     Log in user
        /// </summary>
        /// <param name="logIn"> The log in DTO </param>
        /// <returns> <see cref="Response{T}"/> </returns>
        [HttpPost("Login")]
        public async Task<ActionResult<Response<DtoLogin>>>
        LogIn
        (
           [FromForm] DtoLogin logIn
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _loginService.Login(logIn);

            return StatusCode((int)result.StatusCode, result);
        } 

        #endregion
    }
}