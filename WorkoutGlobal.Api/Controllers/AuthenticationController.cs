using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.DTOs.UserDTOs;

namespace WorkoutGlobal.Api.Controllers
{
    /// <summary>
    /// Represents controller for authorization and registration.
    /// </summary>
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="userManager"></param>
        /// <param name="authManager"></param>
        public AuthenticationController(
            IMapper mapper, 
            UserManager<User> userManager,
            IAuthenticationManager authManager)
        {

        }


        //[HttpPost("authentication/login")]
        //public async Task<IActionResult> Login([FromBody] UserAuthorizationDto userAuthorizationDto)
        //{
            
        //}

    }
}
