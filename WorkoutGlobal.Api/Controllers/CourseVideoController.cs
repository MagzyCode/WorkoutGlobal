using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkoutGlobal.Api.Contracts.RepositoryManagerContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.DTOs.CourseVideoDTOs;

namespace WorkoutGlobal.Api.Controllers
{
    [Route("api/courseVideos")]
    [ApiController]
    public class CourseVideoController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CourseVideoController(
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourseVideo([FromBody] CreationCourseVideoDto creationCourseVideoDto)
        {
            var courseVideo = _mapper.Map<CourseVideo>(creationCourseVideoDto);

            var id = await _repositoryManager.CourseVideosRepository.CreateCourseVideoAsync(courseVideo);

            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourseVideo()
        {
            var courseVideos = await _repositoryManager.CourseVideosRepository.GetAllCourseVideosAsync();

            var courseVideosDto = _mapper.Map<IEnumerable<CourseVideoDto>>(courseVideos);

            return Ok(courseVideosDto);
        }
    }
}
