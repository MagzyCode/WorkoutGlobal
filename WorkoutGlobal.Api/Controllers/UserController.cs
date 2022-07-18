using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Filters.ActionFilters;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.Dto;
using WorkoutGlobal.Api.Models.ErrorModels;

namespace WorkoutGlobal.Api.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UserController(
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpPut("{accountId}")]
        [ModelValidationFilter]
        public async Task<IActionResult> UpdateUser(Guid accountId, [FromBody] UpdationUserDto updationUserDto)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(accountId);

            if (user == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var userCredentials = await _repositoryManager.UserRepository.GetUserCredentialsAsync(accountId);

            // no using mapper because filled in properties of user credentias object overwritten (for example: Id, IsStatusVerify),
            // and IMappingExpression extension don't work for this solution, because we convert dto to model, where dto don't have Id
            await ToUserCredentials(userCredentials, updationUserDto);
            ToUserAccount(user, updationUserDto);

            await _repositoryManager.UserCredentialRepository.UpdateUserCredentialsAsync(userCredentials);

            await _repositoryManager.UserRepository.UpdateUserAsync(user);

            return NoContent();
        }

        private async Task ToUserCredentials(UserCredentials userCredentials, UpdationUserDto updationUserDto)
        {
            userCredentials.PasswordHash = await _repositoryManager.AuthenticationRepository.GenerateHashPasswordAsync(
                updationUserDto.Password, userCredentials.PasswordSalt);
            userCredentials.UserName = updationUserDto.UserName;
            userCredentials.Email = updationUserDto.Email;
            userCredentials.PhoneNumber = updationUserDto.PhoneNumber;
        }

        private static void ToUserAccount(User user, UpdationUserDto updationUserDto)
        {
            user.FirstName = updationUserDto.FirstName;
            user.LastName = updationUserDto.LastName;
            user.Patronymic = updationUserDto.Patronymic;
            user.DateOfBirth = updationUserDto.DateOfBirth;
            user.ResidencePlace = updationUserDto.ResidencePlace;
            user.Sex = updationUserDto.Sex;
            user.Height = updationUserDto.Height;
            user.Weight = updationUserDto.Weight;
            user.SportsActivity = updationUserDto.SportsActivity;
            user.DateOfRegistration = updationUserDto.DateOfRegistration;
            user.ClassificationNumber = updationUserDto.ClassificationNumber;
        }

        [HttpDelete("{accountId}")]
        public async Task<IActionResult> DeleteUser(Guid accountId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(accountId);

            if (user == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            await _repositoryManager.UserRepository.DeleteUserAsync(user);

            return NoContent();
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetUser(Guid accountId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(accountId);

            if (user == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _repositoryManager.UserRepository.GetAllUsersAsync();

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return Ok(usersDto);
        }

        [HttpGet("{accountId}/userCredential")]
        public async Task<IActionResult> GetUserCredentials(Guid accountId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(accountId);

            if (user == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var userCredential = await _repositoryManager.UserRepository.GetUserCredentialsAsync(accountId);

            var userCredentialDto = _mapper.Map<UserCredentialDto>(userCredential);

            return Ok(userCredentialDto);
        }

        [HttpGet("account/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _repositoryManager.UserRepository.GetUserByUsernameAsync(username);

            if (user == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpGet("{accountId}/createdVideos")]
        public async Task<IActionResult> GetTrainerCreatedVideos(Guid accountId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(accountId);

            if (user == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var createdVideos = await _repositoryManager.UserRepository.GetTrainerCreatedVideosAsync(accountId);

            var videosDto = _mapper.Map<IEnumerable<VideoDto>>(createdVideos);

            return Ok(videosDto);
        }


        [HttpGet("{accountId}/createdCourses")]
        public async Task<IActionResult> GetTrainerCreatedCourses(Guid accountId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(accountId);

            if (user == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var createdCourses = await _repositoryManager.UserRepository.GetTrainerCreatedCoursesAsync(accountId);

            var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(createdCourses);

            return Ok(coursesDto);
        }

        [HttpGet("{accountId}/createdEvents")]
        public async Task<IActionResult> GetTrainerCreatedEvents(Guid accountId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(accountId);

            if (user == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var createdEvents = _repositoryManager.UserRepository.GetTrainerCreatedEventsAsync(accountId);

            var eventsDto = _mapper.Map<IEnumerable<SportEventDto>>(createdEvents);

            return Ok(eventsDto);
        }

        [HttpGet("{accountId}/orders")]
        public async Task<IActionResult> GetUserOrders(Guid accountId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(accountId);

            if (user == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var orders = _repositoryManager.UserRepository.GetUserOrdersAsync(accountId);

            var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);

            return Ok(ordersDto);
        }

        [HttpGet("{accountId}/posts")]
        public async Task<IActionResult> GetUserPosts(Guid accountId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(accountId);

            if (user == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var posts = _repositoryManager.UserRepository.GetUserPostsAsync(accountId);

            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            return Ok(postsDto);
        }

        [HttpGet("{accountId}/comments")]
        public async Task<IActionResult> GetUserComments(Guid accountId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(accountId);

            if (user == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var comments = _repositoryManager.UserRepository.GetUserCommentsAsync(accountId);

            var commentsDto = _mapper.Map<IEnumerable<CommentDto>>(comments);

            return Ok(commentsDto);
        }

        [HttpGet("{accountId}/savedCourses")]
        public async Task<IActionResult> GetUserSavedCourses(Guid accountId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(accountId);

            if (user == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var subscribeCourses = await _repositoryManager.UserRepository.GetUserSubscribeCoursesAsync(accountId);

            var subscribeCoursesDto = _mapper.Map<IEnumerable<CourseDto>>(subscribeCourses);

            return Ok(subscribeCoursesDto);
        }

        [HttpGet("{accountId}/savedVideos")]
        public async Task<IActionResult> GetUserSavedVideos(Guid accountId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(accountId);

            if (user == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var savedVideos = await _repositoryManager.UserRepository.GetUserSavedVideosAsync(accountId);

            var savedVideosDto = _mapper.Map<IEnumerable<VideoDto>>(savedVideos);

            return Ok(savedVideosDto);
        }

        [HttpGet("{accountId}/subscribeEvents")]
        public async Task<IActionResult> GetUserSubscribeEvents(Guid accountId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(accountId);

            if (user == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var subscribeEvents = await _repositoryManager.UserRepository.GetUserSubscribeEventsAsync(accountId);

            var subscribeEventsDto = _mapper.Map<IEnumerable<SportEventDto>>(subscribeEvents);

            return Ok(subscribeEventsDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            var userId = await _repositoryManager.UserRepository.CreateUserAsync(user);

            return Created($"api/videos/{userId}", userId);
        }

        [HttpGet("{accountId}/subscriveCourses")]
        public async Task<IActionResult> GetUserSubscribeCourses(Guid accountId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(accountId);

            if (user == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var subscribeCourses = await _repositoryManager.UserRepository.GetUserSubscribeCoursesByIdAsync(accountId);

            var subscribeCoursesDto = _mapper.Map<IEnumerable<SubscribeCourseDto>>(subscribeCourses);

            return Ok(subscribeCoursesDto);

        }

    }
}
