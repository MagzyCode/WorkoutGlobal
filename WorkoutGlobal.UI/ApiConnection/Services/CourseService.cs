using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Services
{
    public class CourseService : BaseService<ICourseService>, ICourseService
    {
        public CourseService(IConfiguration configuration) : base(configuration)
        { }

        public async Task CreateCourseAsync(Course course)
            => await Service.CreateCourseAsync(course);

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
            => await Service.GetAllCoursesAsync();

        public async Task<Course> GetCourseAsync(Guid courseId)
            => await Service.GetCourseAsync(courseId);

        public async Task<IEnumerable<Video>> GetCourseVideosAsync(Guid courseId)
            => await Service.GetCourseVideosAsync(courseId);
    }
}
