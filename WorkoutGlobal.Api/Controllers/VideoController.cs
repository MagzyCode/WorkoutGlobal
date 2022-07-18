using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Filters.ActionFilters;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.Dto;
using WorkoutGlobal.Api.Models.ErrorModels;
using WorkoutGlobal.Api.RequestParameters;

namespace WorkoutGlobal.Api.Controllers
{
    /// <summary>
    /// Base api contoller for video actions.
    /// </summary>
    [Route("api/videos")]
    [ApiController]
    [Produces("application/json")]
    public class VideoController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper; 

        /// <summary>
        /// Ctor for cideo controller.
        /// </summary>
        /// <param name="repositoryManager">Repository manager instance.</param>
        public VideoController(
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVideos()
        {
            var query = HttpContext.Request.Query;

            var videos = query.Count == 0
                ? await _repositoryManager.VideoRepository.GetAllVideosAsync(true)
                : await _repositoryManager.VideoRepository.GetPageVideosAsync(
                    parameters: new VideoParameters(
                        Convert.ToInt32(query["pageNumber"]), Convert.ToInt32(query["pageSize"])),
                    isPublic: true);

            var videosDto = _mapper.Map<IEnumerable<VideoDto>>(videos);

            return Ok(videosDto);
        }

        [HttpGet("{videoId}")]
        public async Task<IActionResult> GetVideo(Guid videoId)
        {
            var video = await _repositoryManager.VideoRepository.GetVideoAsync(videoId);

            if (video == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = 404,
                    Message = "There is no video with such id",
                    Details = "Wrong id."
                });

            var videoDto = _mapper.Map<VideoDto>(video);

            return Ok(videoDto);
        }

        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> CreateVideo([FromBody] CreationVideoDto creationVideoDto)
        {
            var creationVideo = _mapper.Map<Video>(creationVideoDto);

            var videoId = await _repositoryManager.VideoRepository.CreateVideoAsync(creationVideo);

            return Created($"api/videos/{videoId}", videoId);
        }

        [HttpPut("{videoId}")]
        [ModelValidationFilter]
        public async Task<IActionResult> UpdateVideo(Guid videoId, [FromBody] VideoDto videoDto)
        {
            var video = await _repositoryManager.VideoRepository.GetVideoAsync(videoId);

            if (video == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = 404,
                    Message = "There is no video with such id",
                    Details = "Wrong id."
                });

            video.Link = videoDto.Link;
            video.Title = videoDto.Title;
            video.Description = videoDto.Description;
            video.IsPublic = videoDto.IsPublic;

            await _repositoryManager.VideoRepository.UpdateVideoAsync(video);

            return NoContent();
        }

        [HttpDelete("{videoId}")]
        public async Task<IActionResult> DeleteVideo(Guid videoId)
        {
            var video = await _repositoryManager.VideoRepository.GetVideoAsync(videoId);

            if (video == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = 404,
                    Message = "There is no video with such id",
                    Details = "Wrong id."
                });

            await _repositoryManager.VideoRepository.DeleteVideoAsync(video);

            return NoContent();
        }

        [HttpGet("{videoId}/commentsBlock")]
        public async Task<IActionResult> GetVideoCommentsBlock(Guid videoId)
        {
            var video = await _repositoryManager.VideoRepository.GetVideoAsync(videoId);

            if (video == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = 404,
                    Message = "There is no video with such id",
                    Details = "Wrong id."
                });

            var commentsBlock = await _repositoryManager.VideoRepository.GetVideoCommentsBlockAsync(videoId);

            var commentsBlockDto = _mapper.Map<CommentsBlockDto>(commentsBlock);

            return Ok(commentsBlockDto);
        }

        [HttpDelete("purge/{videoId}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Purge(Guid videoId)
        {
            var video = await _repositoryManager.VideoRepository.GetVideoAsync(videoId);

            if (video == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = 404,
                    Message = "There is no video with such id",
                    Details = "Wrong id."
                });

            await _repositoryManager.VideoRepository.DeleteVideoAsync(video);

            return NoContent();
        }

    }
}
