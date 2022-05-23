using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.RequestParameters;
using WorkoutGlobal.UI.ViewModels;

namespace WorkoutGlobal.UI.Controllers
{
    public class VideoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVideoService _videoService;

        public VideoController(
            IMapper mapper,
            IVideoService videoService)
        {
            _mapper = mapper;
            _videoService = videoService;
        }

        public async Task<IActionResult> VideosList()
        {
            // var video = await _videoService.GetVideoAsync(new Guid());

            var videos = await _videoService.GetAllVideosAsync(new VideoParameters());

            var videoViewModels = _mapper.Map<IEnumerable<VideoViewModel>>(videos);

            return View(videoViewModels);
        }
    }
}
