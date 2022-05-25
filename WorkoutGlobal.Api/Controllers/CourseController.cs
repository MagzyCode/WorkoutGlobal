using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkoutGlobal.Api.Contracts.RepositoryManagerContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.DTOs.CourseDTOs;

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

            var courseDto = _mapper.Map<CourseDto>(course);

            return Ok(courseDto);
        }

        [HttpGet("{courseId}/videos")]
        public async Task<IActionResult> GetCourseVideos(Guid courseId)
        {
            var videos = await _repositoryManager.CourseRepository.GetCourseVideosAsync(courseId);

            var videosDto = _mapper.Map<IEnumerable<CourseDto>>(videos);

            return Ok(videosDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);

            await _repositoryManager.CourseRepository.CreateCourseAsync(course);

            return Created($"api/courses/{course.Id}", course.Id);
        }
    }
}
