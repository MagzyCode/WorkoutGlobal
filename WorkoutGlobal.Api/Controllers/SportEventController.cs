using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutGlobal.Api.Contracts.RepositoryManagerContracts;
using WorkoutGlobal.Api.Filters.ActionFilters;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.DTOs.SportEventDTOs;
using WorkoutGlobal.Api.Models.ErrorModels;

namespace WorkoutGlobal.Api.Controllers
{
    [Route("api/sportEvents")]
    [ApiController]
    public class SportEventController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public SportEventController(
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> CreateSportEvent([FromBody] SportEventDto sportEventDto)
        {
            var sportEvent = _mapper.Map<SportEvent>(sportEventDto);

            await _repositoryManager.SportEventRepository.CreateEventAsync(sportEvent);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{sportEventId}")]
        [ModelValidationFilter]
        public async Task<IActionResult> UpdateSportEvent(Guid sportEventId, [FromBody] SportEventDto sportEventDto)
        {
            var sportEvent = await _repositoryManager.SportEventRepository.GetEventAsync(sportEventId);

            if (sportEvent == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no category with such id.",
                    Details = new StackTrace().ToString()
                });

            var updateSportEvent = _mapper.Map<SportEvent>(sportEventDto);

            await _repositoryManager.SportEventRepository.UpdateEventAsync(updateSportEvent);

            return NoContent();
        }

        [HttpDelete("{sportEventId}")]
        public async Task<IActionResult> DeleteSportEvent(Guid sportEventId)
        {
            var sportEvent = await _repositoryManager.SportEventRepository.GetEventAsync(sportEventId);

            if (sportEvent == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no category with such id.",
                    Details = new StackTrace().ToString()
                });

            await _repositoryManager.SportEventRepository.DeleteEventAsync(sportEvent);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSportEvents()
        {
            var sportEvents = await _repositoryManager.SportEventRepository.GetAllEventsAsync();

            var sportEventsDto = _mapper.Map<IEnumerable<SportEventDto>>(sportEvents);

            return Ok(sportEventsDto);
        }

        [HttpGet("{sportEventId}")]
        public async Task<IActionResult> GetSportEvent(Guid sportEventId)
        {
            var sportEvent = await _repositoryManager.SportEventRepository.GetEventAsync(sportEventId);

            var sportEventDto = _mapper.Map<SportEventDto>(sportEvent);

            return Ok(sportEventDto);
        }


    }
}
