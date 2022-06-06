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
        private readonly IConfiguration _configuration;

        public SportEventController(
            IRepositoryManager repositoryManager,
            IMapper mapper,
            IConfiguration configuration)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> CreateSportEvent([FromBody] CreationSportEventDto sportEventDto)
        {
            var (joinLink, hostLink) = await GetZoomLinksAsync(sportEventDto);

            var sportEvent = _mapper.Map<SportEvent>(sportEventDto);
            sportEvent.JoinLink = joinLink;
            sportEvent.HostLink = hostLink;

            await _repositoryManager.SportEventRepository.CreateEventAsync(sportEvent);

            return StatusCode(StatusCodes.Status201Created);
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


        private async Task<(string joinLink, string hostLink)> GetZoomLinksAsync(CreationSportEventDto sportEventDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = _configuration["Zoom:ApiKey"];

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Zoom:ApiKey"],
                Expires = DateTime.UtcNow.AddSeconds(8000),
                SigningCredentials = new SigningCredentials(
                    key: new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(_configuration["Zoom:ApiSecret"])), 
                    algorithm: SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var client = new RestClient($"https://api.zoom.us/v2/users/{_configuration["Zoom:Mail"]}/meetings");
            var request = new RestRequest($"https://api.zoom.us/v2/users/{_configuration["Zoom:Mail"]}/meetings", Method.Post)
            {
                RequestFormat = DataFormat.Json
            };

            var date = $"{sportEventDto.EventStartTime:s}Z";

            request.AddJsonBody(new { agenda = sportEventDto.EventName, start_time=$"{sportEventDto.EventStartTime:s}Z", duration = "90", type = "2" });
            request.AddHeader("authorization", string.Format("Bearer {0}", tokenString));


            RestResponse restResponse = await client.ExecuteAsync(request);
            var jObject = JObject.Parse(restResponse.Content);

            return ((string)jObject["join_url"], (string)jObject["start_url"]);
        }

    }
}
