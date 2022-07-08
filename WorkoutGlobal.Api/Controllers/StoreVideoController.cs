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
        public async Task<IActionResult> CreateStoreVideo([FromBody] CreationStoreVideoDto storeVideoDto)
        {
            var isExisted = await _repositoryManager.StoreVideoRepository.IsStoreVideoExists(storeVideoDto.UserId, storeVideoDto.SavedVideoId);

            if (isExisted)
                return Conflict(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status409Conflict,
                    Message = "There is exist store video with data.",
                    Details = new StackTrace().ToString()
                });

            var storeVideo = _mapper.Map<StoreVideo>(storeVideoDto);

            var storeVideoId = await _repositoryManager.StoreVideoRepository.CreateStoreVideoAsync(storeVideo);

            return Created($"api/videos/{storeVideoId}", storeVideoId);
        }

        [HttpPut("{storeVideoId}")]
        [ModelValidationFilter]
        public async Task<IActionResult> UpdateStoreVideo(Guid storeVideoId, [FromBody] StoreVideoDto storeVideoDto)
        {
            var storeVideo = await _repositoryManager.StoreVideoRepository.GetStoreVideoAsync(storeVideoId);

            if (storeVideo == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
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
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
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
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
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
