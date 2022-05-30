using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.ViewModels;

namespace WorkoutGlobal.UI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IServiceManager _serviceManager;

        public UserController(
            IMapper mapper,
            IServiceManager serviceManager)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SelectProfile()
        {
            var user = await _serviceManager.UserService.GetUserByUsernameAsync(User.Identity.Name);
            var userCredentials = await _serviceManager.UserService.GetUserCredentialsAsync(user.Id);

            var role = await _serviceManager.UserCredentialsServive.GetUserCredentialRoleAsync(userCredentials.Id);

            if (role == "User")
                return RedirectToAction("UserProfile", "User");
            else if (role == "Trainer")
                return RedirectToAction("TrainerProfile", "User");
            return RedirectToAction("AdminProfile", "User");
        }

        public async Task<IActionResult> UserProfile()
        {
            var user = await _serviceManager.UserService.GetUserByUsernameAsync(User.Identity.Name);

            var savedVideos = await _serviceManager.UserService.GetUserSavedVideosAsync(user.Id);
            var savedVideosViewModel = _mapper.Map<IEnumerable<VideoViewModel>>(savedVideos).ToList();

            var subscribeCourses = await _serviceManager.UserService.GetUserSubscribeCoursesAsync(user.Id);
            var subscribeCoursesViewModel = _mapper.Map<IEnumerable<CourseViewModel>>(subscribeCourses).ToList();

            return View(new UserProfileViewModel()
            {
                SavedVideos = savedVideosViewModel,
                SubscriveCourses = subscribeCoursesViewModel
            });
        }

        public async Task<IActionResult> TrainerProfile()
        {
            var user = await _serviceManager.UserService.GetUserByUsernameAsync(User.Identity.Name);

            var createdVideos = await _serviceManager.UserService.GetTrainerCreatedVideosAsync(user.Id);
            var createdVideosViewModel = _mapper.Map<IEnumerable<VideoViewModel>>(createdVideos).ToList();

            var createdCourses = await _serviceManager.UserService.GetTrainerCreatedCoursesAsync(user.Id);
            var createdCoursesViewModel = _mapper.Map<IEnumerable<CourseViewModel>>(createdCourses).ToList();

            return View(new TrainerProfileViewModel()
            {
                CreatedCourses = createdCoursesViewModel,
                CreatedVideos = createdVideosViewModel
            });
        }

        public async Task<IActionResult> AdminProfile()
        {
            var allUsers = await _serviceManager.UserService.GetAllUsersAsync();

            var waitingTrainers = allUsers.Where(x => x.ClassificationNumber != null && x.IsStatusVerify == false).ToList();

            var waitingTrainersViewModel = _mapper.Map<IEnumerable<UserViewModel>>(waitingTrainers).ToList();

            return View(new AdminProfileViewModel()
            {
                WaitingTrainers = waitingTrainersViewModel
            });
        }

        [HttpPost]
        public async Task<IActionResult> AdminProfile(Guid userId)
        {
            var userCredentials = await _serviceManager.UserService.GetUserCredentialsAsync(userId);
            await _serviceManager.UserCredentialsServive.UpdateUserToTrainerAsync(userCredentials.Id);

            return RedirectToAction("Index", "Home");
        }

    }
}
