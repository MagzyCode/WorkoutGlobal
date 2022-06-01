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
        private readonly IServiceManager _serviceManager;

        public VideoController(
            IMapper mapper,
            IServiceManager serviceManager)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> VideosList()
        {
            var videos = await _serviceManager.VideoService.GetAllVideosAsync(new VideoParameters());

            var videoViewModels = _mapper.Map<IEnumerable<VideoWithCommentsAndSubscriptionViewModel>>(videos);

            return View(videoViewModels);
        }

        public async Task<IActionResult> ShowVideo(Guid videoId, string username)
        {
            var video = await _serviceManager.VideoService.GetVideoAsync(videoId);

            var videoViewModel = _mapper.Map<VideoWithCommentsAndSubscriptionViewModel>(video);

            var commentsBlock = await _serviceManager.VideoService.GetVideoCommentsBlockAsync(videoId);
 
            var comments = await _serviceManager.CommentsBlockService.GetBlockCommentsAsync(commentsBlock.Id);
            var commentsViewModel = _mapper.Map<IEnumerable<CommentViewModel>>(comments);

            var user = await _serviceManager.UserService.GetUserByUsernameAsync(username);

            var savedVideos = await _serviceManager.UserService.GetUserSavedVideosAsync(user.Id);

            var isSubscribe = savedVideos.Any(x => x.Id == video.Id);

            videoViewModel.IsSubscribe = isSubscribe;
            videoViewModel.Comments = commentsViewModel.ToList();

            return View(videoViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveComment(VideoWithCommentsAndSubscriptionViewModel videoWithCommentsViewModel)
        {
            var commentsBlock = await _serviceManager.VideoService.GetVideoCommentsBlockAsync(videoWithCommentsViewModel.Id);

            var comment = new Comment()
                {
                    CommentText = videoWithCommentsViewModel.AdditionComment,
                    CommentatorName = User.Identity.Name,
                    CommentatorId = videoWithCommentsViewModel.UserId,
                    CommentsBlockId = commentsBlock.Id
                };

            await _serviceManager.CommentService.CreateCommentAsync(comment);
            var comments = await _serviceManager.CommentsBlockService.GetBlockCommentsAsync(commentsBlock.Id);
            var commentsViewModel = _mapper.Map<IEnumerable<CommentViewModel>>(comments);

            videoWithCommentsViewModel.Comments = commentsViewModel.ToList();

            return View("ShowVideo", videoWithCommentsViewModel);
        }

        public async Task<IActionResult> AddVideo()
        {
            var categoties = await _serviceManager.CategoryService.GetAllCategoriesAsync();
            var categotiesNames = categoties.Select(x => x.CategoryName).ToList();

            return View(new CreationVideoViewModel()
            {
                Categories = categotiesNames
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddVideo(string username, CreationVideoViewModel creationVideoViewModel)
        {
            creationVideoViewModel.Link = creationVideoViewModel.Link.Replace("youtu.be/", "youtube.com/embed/");

            var user = await _serviceManager.UserService.GetUserByUsernameAsync(username);

            creationVideoViewModel.UserId = user.Id;

            var category = await _serviceManager.CategoryService.GetCategoryByNameAsync(creationVideoViewModel.CategoryName);

            var video = _mapper.Map<Video>(creationVideoViewModel);
            video.CategoryId = category.Id;

            await _serviceManager.VideoService.CreateVideoAsync(video);

            return RedirectToAction("VideosList", "Video");
        }

        [HttpPost]
        public async Task<IActionResult> SubscribeVideo(VideoWithCommentsAndSubscriptionViewModel videoWithCommentsViewModel)
        {
            var storeVideo = new StoreVideo()
            {
                SavedVideoId = videoWithCommentsViewModel.Id,
                UserId = videoWithCommentsViewModel.SubscriberId
            };

            await _serviceManager.StoreVideoService.CreateStoreVideoAsync(storeVideo);

            var commentsBlock = await _serviceManager.VideoService.GetVideoCommentsBlockAsync(videoWithCommentsViewModel.Id);
            var comments = await _serviceManager.CommentsBlockService.GetBlockCommentsAsync(commentsBlock.Id);
            var commentsViewModel = _mapper.Map<IEnumerable<CommentViewModel>>(comments);

            videoWithCommentsViewModel.Comments = commentsViewModel.ToList();
            videoWithCommentsViewModel.IsSubscribe = true;

            return View("ShowVideo", videoWithCommentsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UnsubscribeVideo(VideoWithCommentsAndSubscriptionViewModel videoWithCommentsViewModel)
        {
            var storeVideos = await _serviceManager.StoreVideoService.GetAllStoreVideosAsync();
            var storeVideo = storeVideos.Where(x => x.SavedVideoId == videoWithCommentsViewModel.Id && x.UserId == videoWithCommentsViewModel.SubscriberId).FirstOrDefault();

            await _serviceManager.StoreVideoService.DeleteStoreVideoAsync(storeVideo.Id);

            var commentsBlock = await _serviceManager.VideoService.GetVideoCommentsBlockAsync(videoWithCommentsViewModel.Id);
            var comments = await _serviceManager.CommentsBlockService.GetBlockCommentsAsync(commentsBlock.Id);
            var commentsViewModel = _mapper.Map<IEnumerable<CommentViewModel>>(comments);

            videoWithCommentsViewModel.Comments = commentsViewModel.ToList();

            return View("ShowVideo", videoWithCommentsViewModel);
        }
    }
}
