using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Services
{
    public class CourseVideoService : BaseService<ICourseVideoService>, ICourseVideoService
    {
        public CourseVideoService(IConfiguration configuration) : base(configuration)
        { }

        public async Task<Guid> CreateCourseVideoAsync(CourseVideo courseVideo)
            => await Service.CreateCourseVideoAsync(courseVideo);

        public async Task<IEnumerable<CourseVideo>> GetAllCourseVideoAsync()
            => await Service.GetAllCourseVideoAsync();
    }
}
