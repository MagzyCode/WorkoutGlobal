using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutGlobal.Api.Contracts.RepositoryManagerContracts;
using WorkoutGlobal.Api.Filters.ActionFilters;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.DTOs.SubscribeCourseDTOs;
using WorkoutGlobal.Api.Models.ErrorModels;

namespace WorkoutGlobal.Api.Controllers
{
    [Route("api/subscribeCourses")]
    [ApiController]
    public class SubscribeCourseController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public SubscribeCourseController(
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> CreateSubscribeCourse([FromBody] SubscribeCourseDto subscribeCourseDto)
        {
            var subscribeCourse = _mapper.Map<SubscribeCourse>(subscribeCourseDto);

            await _repositoryManager.SubscribeCourseRepository.CreateSubscribeCourseAsync(subscribeCourse);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{subscribeCourseId}")]
        [ModelValidationFilter]
        public async Task<IActionResult> UpdateSubscribeCourse(Guid subscribeCourseId, [FromBody] SubscribeCourseDto subscribeCourseDto)
        {
            var subscribeCourse = await _repositoryManager.SubscribeCourseRepository.GetSubscribeCourseAsync(subscribeCourseId);

            if (subscribeCourse == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no category with such id.",
                    Details = new StackTrace().ToString()
                });

            var updateSubscribeCourse = _mapper.Map<SubscribeCourse>(subscribeCourseDto);

            await _repositoryManager.SubscribeCourseRepository.UpdateSubscribeCourseAsync(updateSubscribeCourse);

            return NoContent();
        }

        [HttpDelete("{subscribeCourseId}")]
        public async Task<IActionResult> DeleteSubscribeCourse(Guid subscribeCourseId)
        {
            var subscribeCourse = await _repositoryManager.SubscribeCourseRepository.GetSubscribeCourseAsync(subscribeCourseId);

            if (subscribeCourse == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no category with such id.",
                    Details = new StackTrace().ToString()
                });

            await _repositoryManager.SubscribeCourseRepository.DeleteSubscribeCourseAsync(subscribeCourse);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubscribeCourses()
        {
            var models = await _repositoryManager.SubscribeCourseRepository.GetAllSubscribeCourseAsync();

            var modelsDto = _mapper.Map<IEnumerable<SubscribeCourseDto>>(models);

            return Ok(modelsDto);
        }

        [HttpGet("{subscribeCourseId}")]
        public async Task<IActionResult> GetSubscribeCourse(Guid subscribeCourseId)
        {
            var subscribeCourse = await _repositoryManager.SubscribeCourseRepository.GetSubscribeCourseAsync(subscribeCourseId);

            if (subscribeCourse == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no category with such id.",
                    Details = new StackTrace().ToString()
                });

            var subscribeCourseDto = _mapper.Map<SubscribeCourseDto>(subscribeCourse);

            return Ok(subscribeCourseDto);
        }
    }
}
