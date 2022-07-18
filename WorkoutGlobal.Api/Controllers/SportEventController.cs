using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Extensions;
using WorkoutGlobal.Api.Filters.ActionFilters;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.Dto;
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
        public async Task<IActionResult> CreateSportEvent([FromBody] CreationSportEventDto creationSportEventDto)
        {
            var userAccount = await _repositoryManager.UserRepository.GetUserAsync(creationSportEventDto.TrainerId);
            var category = await _repositoryManager.CategoryRepository.GetCategoryAsync(creationSportEventDto.CategoryId);

            if (userAccount == null || category == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no sport event with such id.",
                    Details = new StackTrace().ToString()
                });

            var (joinLink, hostLink) = await creationSportEventDto.GetZoomLinksAsync();

            var sportEvent = _mapper.Map<SportEvent>(creationSportEventDto);
            sportEvent.JoinLink = joinLink;
            sportEvent.HostLink = hostLink;

            var sportEventId = await _repositoryManager.SportEventRepository.CreateEventAsync(sportEvent);

            return Created($"api/videos/{sportEventId}", sportEventId);
        }

        [HttpPut("{sportEventId}")]
        [ModelValidationFilter]
        public async Task<IActionResult> UpdateSportEvent(Guid sportEventId, [FromBody] SportEventDto sportEventDto)
        {
            var sportEvent = await _repositoryManager.SportEventRepository.GetEventAsync(sportEventId);

            if (sportEvent == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no sport event with such id.",
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
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no sport event with such id.",
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

            if (sportEvent == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no sport event with such id.",
                    Details = new StackTrace().ToString()
                });

            var sportEventDto = _mapper.Map<SportEventDto>(sportEvent);

            return Ok(sportEventDto);
        }

        [HttpGet("{sportEventId}/subscribers")]
        public async Task<IActionResult> GetSportEventSubscribers(Guid sportEventId)
        {
            var sportEvent = await _repositoryManager.SportEventRepository.GetEventAsync(sportEventId);

            if (sportEvent == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no sport event with such id.",
                    Details = new StackTrace().ToString()
                });

            var users = await _repositoryManager.SportEventRepository.GetEventSubscribersAsync(sportEventId);

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return Ok(usersDto);
        }
    }
}
