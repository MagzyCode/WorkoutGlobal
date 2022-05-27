using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.Models;
using WorkoutGlobal.UI.RequestParameters;
using WorkoutGlobal.UI.ViewModels;

namespace WorkoutGlobal.UI.Controllers
{
    public class VideoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVideoService _videoService;
        private readonly ICommentsBlockService _commentsBlockService;
        private readonly ICommentService _commentService;

        public VideoController(
            IMapper mapper,
            IVideoService videoService,
            ICommentsBlockService commentsBlockService,
            ICommentService commentService)
        {
            _mapper = mapper;
            _videoService = videoService;
            _commentsBlockService = commentsBlockService;
            _commentService = commentService;
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

        [HttpPost]
        public async Task<IActionResult> SaveComment(VideoWithCommentsViewModel videoWithCommentsViewModel)
        {
            var commentsBlock = await _commentsBlockService.GetCommentsBlockByVideoIdAsync(videoWithCommentsViewModel.Id);

            var comment = new Comment()
                {
                    CommentText = videoWithCommentsViewModel.AdditionComment,
                    CommentatorName = User.Identity.Name,
                    CommentatorId = videoWithCommentsViewModel.UserId,
                    CommentsBlockId = commentsBlock.Id
                };

            await _commentService.CreateCommentAsync(comment);
            var comments = await _commentsBlockService.GetBlockCommentsAsync(commentsBlock.Id);
            var commentsViewModel = _mapper.Map<IEnumerable<CommentViewModel>>(comments);

            videoWithCommentsViewModel.Comments = commentsViewModel.ToList();

            return View("ShowVideo", videoWithCommentsViewModel);
        }

        public IActionResult AddVideo()
        {
            return View(new CreationVideoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddVideo(CreationVideoViewModel creationVideoViewModel)
        {
            creationVideoViewModel.Link = creationVideoViewModel.Link.Replace("youtu.be/", "youtube.com/embed/");
            creationVideoViewModel.UserId = Guid.Parse("4e14c6d2-cae1-43ba-bd32-0f2dc2225f5a");
            var video = _mapper.Map<Video>(creationVideoViewModel);

            await _videoService.AddVideoAsync(video);

            return RedirectToAction("VideosList", "Video");
        }
    }
}
