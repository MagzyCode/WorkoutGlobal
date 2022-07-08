using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutGlobal.Api.Attributes;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Filters.ActionFilters;
using WorkoutGlobal.Api.Models.Dto;
using WorkoutGlobal.Api.Models.ErrorModels;

namespace WorkoutGlobal.Api.Controllers
{
    /// <summary>
    /// Represents controller for authorization and registration.
    /// </summary>
    [Route("api/authentication")]
    [ApiController]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        /// <summary>
        /// Ctor for authentication controller.
        /// </summary>
        /// <param name="repositoryManager">Repository manager instance.</param>
        public AuthenticationController(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        /// <summary>
        /// Authenticate user credentials.
        /// </summary>
        /// <param name="userAuthorizationDto">User credentials.</param>
        /// <returns>If credentials are correct sent JWT-token, otherwise sent Unauthorized.</returns>
        /// <response code="200">User was successfully authenticate.</response>
        /// <response code="401">Incoming model credentials isn't valid.</response>
        /// <response code="500">Something going wrong on server.</response>
        [HttpPost("login")]
        [ModelValidationFilter]
        [ProducesResponseType(type: typeof(string), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(ErrorDetails), statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(type: typeof(ErrorDetails), statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthorizationDto userAuthorizationDto)
        {
            var isUserValid = await _repositoryManager.AuthenticationRepository.ValidateUserAsync(userAuthorizationDto);

            if (!isUserValid)
                return Unauthorized(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "Authenticate was failed. Try another user name or password",
                    Details = new StackTrace().ToString()
                });

            var token = _repositoryManager.AuthenticationRepository.CreateToken(userAuthorizationDto);
            
            return Ok(token);
        }

        /// <summary>
        /// Registrate user in system.
        /// </summary>
        /// <param name="userRegistrationDto"></param>
        /// <returns>If user already exists in system return BadRequest status with error model, if don't exists return Created status.</returns>
        /// <response code="200">User was successfully registered.</response>
        /// <response code="400">Incoming model already exists in system.</response>
        /// <response code="500">Something going wrong on server.</response>
        [HttpPost("registration")]
        [ModelValidationFilter]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(type: typeof(ErrorDetails), statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(type: typeof(ErrorDetails), statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Registrate([FromBody] UserRegistrationDto userRegistrationDto)
        {
            var isUserExisted = _repositoryManager.AuthenticationRepository.IsUserExisted(userRegistrationDto);

            if (isUserExisted)
                return Unauthorized(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "User already exists.",
                    Details = new StackTrace().ToString()
                });

            var userId = await _repositoryManager.AuthenticationRepository.RegistrateUserAsync(userRegistrationDto);

            return Created($"api/userCredentials/{userId}", userId);
        }

        [HttpDelete("purge/{userCredentialsId}")]
        [TestApi]
        public async Task<IActionResult> Purge(string userCredentialsId)
        {
            var userCredentials = await _repositoryManager.UserCredentialRepository.GetUserCredentialsAsync(userCredentialsId);

            if (userCredentials == null)
                return NotFound(new ErrorDetails() 
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "User don't exists.",
                    Details = new StackTrace().ToString()
                });

            var userAccount = await _repositoryManager.UserRepository.GetUserByUsernameAsync(userCredentials.UserName);
            await _repositoryManager.UserRepository.DeleteUserAsync(userAccount);

            await _repositoryManager.AuthenticationRepository.Purge(userCredentials);

            return NoContent();
        }
    }
}
