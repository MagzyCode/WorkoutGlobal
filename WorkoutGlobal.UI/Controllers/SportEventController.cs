using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.ViewModels;

namespace WorkoutGlobal.UI.Controllers
{
    public class SportEventController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IServiceManager _serviceManager;

        public SportEventController(
            IMapper mapper,
            IServiceManager serviceManager)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> SportEventList()
        {
            var models = await _serviceManager.SportEventService.GetAllSportEventsAsync();

            var modelsViewModel = _mapper.Map<IEnumerable<SportEventViewModel>>(models);

            return View(modelsViewModel);
        }
    }
}
