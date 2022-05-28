using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutGlobal.Api.Contracts.RepositoryManagerContracts;
using WorkoutGlobal.Api.Filters.ActionFilters;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.DTOs.StoreVideoDTOs;
using WorkoutGlobal.Api.Models.ErrorModels;

namespace WorkoutGlobal.Api.Controllers
{
    [Route("api/storeVideos")]
    [ApiController]
    public class StoreVideoController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public StoreVideoController(
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> CreateStoreVideo([FromBody] StoreVideoDto storeVideoDto)
        {
            var storeVideo = _mapper.Map<StoreVideo>(storeVideoDto);

            await _repositoryManager.StoreVideoRepository.CreateStoreVideoAsync(storeVideo);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{storeVideoId}")]
        [ModelValidationFilter]
        public async Task<IActionResult> UpdateStoreVideo(Guid storeVideoId, [FromBody] StoreVideoDto storeVideoDto)
        {
            var storeVideo = await _repositoryManager.StoreVideoRepository.GetStoreVideoAsync(storeVideoId);

            if (storeVideo == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no store video with such id.",
                    Details = new StackTrace().ToString()
                });

            var updateStoreVideo = _mapper.Map<StoreVideo>(storeVideoDto);

            await _repositoryManager.StoreVideoRepository.UpdateStoreVideoAsync(updateStoreVideo);

            return NoContent();
        }

        [HttpDelete("{storeVideoId}")]
        public async Task<IActionResult> DeleteStoreVideo(Guid storeVideoId)
        {
            var storeVideo = await _repositoryManager.StoreVideoRepository.GetStoreVideoAsync(storeVideoId);

            if (storeVideo == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no store video with such id.",
                    Details = new StackTrace().ToString()
                });

            await _repositoryManager.StoreVideoRepository.DeleteStoreVideoAsync(storeVideo);

            return NoContent();
        }

        [HttpGet("{storeVideoId}")]
        public async Task<IActionResult> GetStoreVideo(Guid storeVideoId)
        {
            var storeVideo = await _repositoryManager.StoreVideoRepository.GetStoreVideoAsync(storeVideoId);

            if (storeVideo == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no store video with such id.",
                    Details = new StackTrace().ToString()
                });

            var storeVideoDto = _mapper.Map<StoreVideoDto>(storeVideo);

            return Ok(storeVideoDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStoreVideos()
        {
            var storeVideos = await _repositoryManager.StoreVideoRepository.GetAllStoreVideosAsync();

            var storeVideosDto = _mapper.Map<IEnumerable<StoreVideoDto>>(storeVideos);

            return Ok(storeVideosDto);
        }
    }
}
