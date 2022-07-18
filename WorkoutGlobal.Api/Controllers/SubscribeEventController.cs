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
    [Route("api/subscribeEvents")]
    [ApiController]
    public class SubscribeEventController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public SubscribeEventController(
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> CreateSubscribeEvent([FromBody] CreationSubscribeEventDto subscribeEventDto)
        {
            var isExisted = await _repositoryManager.SubscribeEventRepository.IsSportEventSubscriptionExists(
                userId: subscribeEventDto.UserId,
                eventId: subscribeEventDto.EventId);

            if (isExisted)
                return Conflict(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status409Conflict,
                    Message = "There is course subscription with such data.",
                    Details = new StackTrace().ToString()
                });

            var subscribeEvent = _mapper.Map<SubscribeEvent>(subscribeEventDto);

            var subscribeEventId = await _repositoryManager.SubscribeEventRepository.CreateSubscribeEventAsync(subscribeEvent);

            return Created($"api/videos/{subscribeEventId}", subscribeEventId);
        }

        [HttpPut("{subscribeEventId}")]
        [ModelValidationFilter]
        public async Task<IActionResult> UpdateSubscribeEvent(Guid subscribeEventId, [FromBody] SubscribeEventDto subscribeEventDto)
        {
            var subscribeEvent = await _repositoryManager.SubscribeEventRepository.GetSubscribeEventAsync(subscribeEventId);

            if (subscribeEvent == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no subscribe video with such id.",
                    Details = new StackTrace().ToString()
                });

            var updateSubscribeEvent = _mapper.Map<SubscribeEvent>(subscribeEventDto);

            await _repositoryManager.SubscribeEventRepository.UpdateSubscribeEventAsync(updateSubscribeEvent);

            return NoContent();
        }

        [HttpDelete("{subscribeEventId}")]
        public async Task<IActionResult> DeleteSubscribeEvent(Guid subscribeEventId)
        {
            var subscribeEvent = await _repositoryManager.SubscribeEventRepository.GetSubscribeEventAsync(subscribeEventId);

            if (subscribeEvent == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no subscribe video with such id.",
                    Details = new StackTrace().ToString()
                });

            await _repositoryManager.SubscribeEventRepository.DeleteSubscribeEventAsync(subscribeEvent);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubscribeEvents()
        {
            var models = await _repositoryManager.SubscribeEventRepository.GetAllSubscribeEventsAsync();

            var modelsDto = _mapper.Map<IEnumerable<SubscribeEventDto>>(models);

            return Ok(modelsDto);
        }

        [HttpGet("{subscribeEventId}")]
        public async Task<IActionResult> GetSubscribeEvent(Guid subscribeEventId)
        {
            var model = await _repositoryManager.SubscribeEventRepository.GetSubscribeEventAsync(subscribeEventId);

            var modelDto = _mapper.Map<SubscribeEventDto>(model);

            return Ok(modelDto);
        }
    }
}
