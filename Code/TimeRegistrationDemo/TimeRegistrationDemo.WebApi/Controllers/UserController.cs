using Microsoft.AspNetCore.Mvc;
using TimeRegistrationDemo.Services.Interfaces;

namespace TimeRegistrationDemo.WebApi
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IGetAllUsersForAuthenticationService GetAllUsersForAuthenticationService;

        public UserController(IGetAllUsersForAuthenticationService getAllUsersForAuthenticationService)
        {
            GetAllUsersForAuthenticationService = getAllUsersForAuthenticationService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(GetAllUsersForAuthenticationService.GetAllUsers());
        }
    }
}
