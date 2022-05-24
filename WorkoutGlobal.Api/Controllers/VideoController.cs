using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkoutGlobal.Api.Contracts.RepositoryManagerContracts;
using WorkoutGlobal.Api.Filters.ActionFilters;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.DTOs.CommentsBlockDTOs;
using WorkoutGlobal.Api.Models.DTOs.VideoDTOs;
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

            var videos = query.Count != 0
                ? await _repositoryManager.VideoRepository.GetPageVideosAsync(
                   parameters: new VideoParameters(Convert.ToInt32(query["pageNumber"]), Convert.ToInt32(query["pageSize"])), 
                   isPublic: true)
                : await _repositoryManager.VideoRepository.GetAllVideosAsync(true);

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
                    Message = "There is no fridge model with such fridgeModelId",
                    Details = "Wrong id."
                });

            var videoDto = _mapper.Map<VideoDto>(video);

            return Ok(videoDto);
        }

        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> AddVideo([FromBody] CreationVideoDto creationVideoDto)
        {
            var creationVideo = _mapper.Map<Video>(creationVideoDto);

            await _repositoryManager.VideoRepository.AddVideoAsync(creationVideo);
            await _repositoryManager.CommentsBlockRepository.CreateCommentBlockAsync(new CommentsBlock() { CommentedVideoId = creationVideo.Id });

            return Created($"api/videos/{creationVideo.Id}", creationVideo.Id);
        }
    }
}
