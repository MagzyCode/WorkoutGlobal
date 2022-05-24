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
        private readonly ICommentsBlockService _commentsBlockService;

        public VideoController(
            IMapper mapper,
            IVideoService videoService,
            ICommentsBlockService commentsBlockService)
        {
            _mapper = mapper;
            _videoService = videoService;
            _commentsBlockService = commentsBlockService;
        }

        public async Task<IActionResult> VideosList()
        {
            var videos = await _videoService.GetAllVideosAsync(new VideoParameters());

            var videoViewModels = _mapper.Map<IEnumerable<VideoWithCommentsViewModel>>(videos);

            return View(videoViewModels);
        }

        public async Task<IActionResult> ShowVideo(Guid videoId)
        {
            var video = await _videoService.GetVideoAsync(videoId);

            var videoViewModel = _mapper.Map<VideoWithCommentsViewModel>(video);

            var commentsBlock = await _commentsBlockService.GetCommentsBlockByVideoIdAsync(videoId);
            var comments = await _commentsBlockService.GetBlockCommentsAsync(commentsBlock.Id);
            var commentsViewModel = _mapper.Map<IEnumerable<CommentViewModel>>(comments);

            videoViewModel.Comments = commentsViewModel.ToList();

            return View(videoViewModel);
        }
    }
}
