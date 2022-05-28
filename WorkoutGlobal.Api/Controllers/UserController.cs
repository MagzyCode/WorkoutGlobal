using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutGlobal.Api.Contracts.RepositoryManagerContracts;
using WorkoutGlobal.Api.Filters.ActionFilters;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.DTOs.CommentDTOs;
using WorkoutGlobal.Api.Models.DTOs.CourseDTOs;
using WorkoutGlobal.Api.Models.DTOs.OrderDTOs;
using WorkoutGlobal.Api.Models.DTOs.PostDTOs;
using WorkoutGlobal.Api.Models.DTOs.SportEventDTOs;
using WorkoutGlobal.Api.Models.DTOs.UserCredentialDTOs;
using WorkoutGlobal.Api.Models.DTOs.UserDTOs;
using WorkoutGlobal.Api.Models.DTOs.VideoDTOs;
using WorkoutGlobal.Api.Models.ErrorModels;

namespace WorkoutGlobal.Api.Controllers
{
    [Route("api/users")]
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

        [HttpPut("{userId}")]
        [ModelValidationFilter]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserDto userDto)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(userId);

            if (user == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var updateUser = _mapper.Map<User>(userDto);

            await _repositoryManager.UserRepository.UpdateUserAsync(updateUser);

            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(userId);

            if (user == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            await _repositoryManager.UserRepository.DeleteUserAsync(user);

            return NoContent();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(userId);

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

        [HttpGet("{userId}/userCredential")]
        public async Task<IActionResult> GetUserCredentials(Guid userId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(userId);

            if (user == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var userCredential = await _repositoryManager.UserRepository.GetUserCredentialsAsync(userId);

            var userCredentialDto = _mapper.Map<UserCredentialDto>(userCredential);

            return Ok(userCredentialDto);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _repositoryManager.UserRepository.GetUserByUsernameAsync(username);

            if (user == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpGet("{userId}/createdVideos")]
        public async Task<IActionResult> GetTrainerCreatedVideos(Guid userId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(userId);

            if (user == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var createdVideos = _repositoryManager.UserRepository.GetTrainerCreatedVideosAsync(userId);

            var videosDto = _mapper.Map<IEnumerable<VideoDto>>(createdVideos);

            return Ok(videosDto);
        }


        [HttpGet("{userId}/createdCourses")]
        public async Task<IActionResult> GetTrainerCreatedCourses(Guid userId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(userId);

            if (user == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var createdCourses = _repositoryManager.UserRepository.GetTrainerCreatedCoursesAsync(userId);

            var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(createdCourses);

            return Ok(coursesDto);
        }

        [HttpGet("{userId}/createdEvents")]
        public async Task<IActionResult> GetTrainerCreatedEvents(Guid userId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(userId);

            if (user == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var createdEvents = _repositoryManager.UserRepository.GetTrainerCreatedEventsAsync(userId);

            var eventsDto = _mapper.Map<IEnumerable<SportEventDto>>(createdEvents);

            return Ok(eventsDto);
        }

        [HttpGet("{userId}/orders")]
        public async Task<IActionResult> GetUserOrders(Guid userId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(userId);

            if (user == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var orders = _repositoryManager.UserRepository.GetUserOrdersAsync(userId);

            var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);

            return Ok(ordersDto);
        }

        [HttpGet("{userId}/posts")]
        public async Task<IActionResult> GetUserPosts(Guid userId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(userId);

            if (user == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var posts = _repositoryManager.UserRepository.GetUserPostsAsync(userId);

            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            return Ok(postsDto);
        }

        [HttpGet("{userId}/comments")]
        public async Task<IActionResult> GetUserComments(Guid userId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(userId);

            if (user == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var comments = _repositoryManager.UserRepository.GetUserCommentsAsync(userId);

            var commentsDto = _mapper.Map<IEnumerable<CommentDto>>(comments);

            return Ok(commentsDto);
        }

        [HttpGet("{userId}/subscribeCourses")]
        public async Task<IActionResult> GetUserSubscribeCourses(Guid userId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(userId);

            if (user == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var subscribeCourses = await _repositoryManager.UserRepository.GetUserSubscribeCoursesAsync(userId);
            
            var subscribeCoursesDto = _mapper.Map<IEnumerable<CourseDto>>(subscribeCourses);

            return Ok(subscribeCoursesDto);
        }

        [HttpGet("{userId}/savedVideos")]
        public async Task<IActionResult> GetUserSavedVideos(Guid userId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(userId);

            if (user == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var savedVideos = await _repositoryManager.UserRepository.GetUserSavedVideosAsync(userId);

            var savedVideosDto = _mapper.Map<IEnumerable<VideoDto>>(savedVideos);

            return Ok(savedVideosDto);
        }

        [HttpGet("{userId}/subscribeEvents")]
        public async Task<IActionResult> GetUserSubscribeEvents(Guid userId)
        {
            var user = await _repositoryManager.UserRepository.GetUserAsync(userId);

            if (user == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no user with such id.",
                    Details = new StackTrace().ToString()
                });

            var subscribeEvents = await _repositoryManager.UserRepository.GetUserSubscribeEventsAsync(userId);

            var subscribeEventsDto = _mapper.Map<IEnumerable<VideoDto>>(subscribeEvents);

            return Ok(subscribeEventsDto);
        }

    }
}
