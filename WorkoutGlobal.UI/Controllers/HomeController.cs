using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.Filters.ActionFilters;
using WorkoutGlobal.UI.Models;
using WorkoutGlobal.UI.ViewModels;

namespace WorkoutGlobal.UI.Controllers
{
    /// <summary>
    /// Home controller.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IServiceManager _serviceManager;

        /// <summary>
        /// Ctor for home controller.
        /// </summary>
        public HomeController(
            IMapper mapper,
            IServiceManager serviceManager)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
        }

        /// <summary>
        /// Represents action-method for user log in.
        /// </summary>
        /// <returns>Login view.</returns>
        public IActionResult Login()
        {
            return View(new UserAuthorizationViewModel());
        }

        /// <summary>
        /// Represents POST action-method for user log in.
        /// </summary>
        /// <param name="userAuthorizationViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> Login(UserAuthorizationViewModel userAuthorizationViewModel)
        {
            var authenticationUser = _mapper.Map<AuthenticationUser>(userAuthorizationViewModel);

            await Authenticate(authenticationUser);

            return RedirectToAction("VideosList", "Video");
        }

        /// <summary>
        /// Represents action method for registration page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Registration()
        {
            return View(new UserRegistrationViewModel());
        }

        /// <summary>
        /// Represents POST action method for registration page.
        /// </summary>
        /// <param name="userRegistrationViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> Registration(UserRegistrationViewModel userRegistrationViewModel)
        {
            var registrationUser = _mapper.Map<RegistrationUser>(userRegistrationViewModel);
            
            await _serviceManager.AuthenticationService.RegistrateAsync(registrationUser);
            var authenticationUser = _mapper.Map<AuthenticationUser>(registrationUser);
            await Authenticate(authenticationUser);

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Represents action-method for home page.
        /// </summary>
        /// <returns>Home page view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Home");
        }

        /// <summary>
        /// Authenticate user by credentials.
        /// </summary>
        /// <param name="authenticationUser">User credentials.</param>
        /// <returns>A task that represents asynchronous Authenticate operation.</returns>
        private async Task Authenticate(AuthenticationUser authenticationUser)
        {
            var token = await _serviceManager.AuthenticationService.AuthenticateAsync(authenticationUser);

            var claims = new List<Claim>
            {
                new Claim("Token", token),
                new Claim(ClaimsIdentity.DefaultNameClaimType, authenticationUser.UserName)
            };

            ClaimsIdentity id = new(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}