using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Filters.ActionFilters;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.Dto;
using WorkoutGlobal.Api.Models.ErrorModels;

namespace WorkoutGlobal.Api.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CourseController(
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _repositoryManager.CourseRepository.GetAllCoursesAsync();

            var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(courses);

            return Ok(coursesDto);
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourse(Guid courseId)
        {
            var course = await _repositoryManager.CourseRepository.GetCourseAsync(courseId);

            if (course == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no course with such id.",
                    Details = new StackTrace().ToString()
                });

            var courseDto = _mapper.Map<CourseDto>(course);

            return Ok(courseDto);
        }

        [HttpGet("{courseId}/videos")]
        public async Task<IActionResult> GetCourseVideos(Guid courseId)
        {
            var course = await _repositoryManager.CourseRepository.GetCourseAsync(courseId);

            if (course == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no course with such id.",
                    Details = new StackTrace().ToString()
                });

            var videos = await _repositoryManager.CourseRepository.GetCourseVideosAsync(courseId);

            var videosDto = _mapper.Map<IEnumerable<VideoDto>>(videos);

            return Ok(videosDto);
        }

        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> CreateCourse([FromBody] CreationCourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);

            var courseId = await _repositoryManager.CourseRepository.CreateCourseAsync(course);

            return Created($"api/videos/{courseId}", courseId);
        }

        [HttpPut("{courseId}")]
        [ModelValidationFilter]
        public async Task<IActionResult> UpdateCourse(Guid courseId, [FromBody] CreationCourseDto courseDto)
        {
            var course = await _repositoryManager.CourseRepository.GetCourseAsync(courseId);

            if (course == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no course with such id.",
                    Details = new StackTrace().ToString()
                });

            var updateCourse = _mapper.Map<Course>(courseDto);

            await _repositoryManager.CourseRepository.UpdateCourseAsync(updateCourse);

            return NoContent();
        }

        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCourse(Guid courseId)
        {
            var course = await _repositoryManager.CourseRepository.GetCourseAsync(courseId);

            if (course == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no course with such id.",
                    Details = new StackTrace().ToString()
                });

            await _repositoryManager.CourseRepository.DeleteCourseAsync(course);

            return NoContent();
        }

        [HttpGet("{courseId}/subscribers")]
        public async Task<IActionResult> GetCourseSubscribers(Guid courseId)
        {
            var course = await _repositoryManager.CourseRepository.GetCourseAsync(courseId);

            if (course == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no course with such id.",
                    Details = new StackTrace().ToString()
                });

            var subscribers = await _repositoryManager.CourseRepository.GetCourseSubscribersAsync(courseId);

            var subscribersDto = _mapper.Map<IEnumerable<UserDto>>(subscribers);

            return Ok(subscribersDto);
        }
    }
}
