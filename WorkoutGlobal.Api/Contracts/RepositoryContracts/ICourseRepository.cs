using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Contracts.RepositoryContracts
{
    public interface ICourseRepository
    {
        public Task<IEnumerable<Course>> GetAllCoursesAsync();

        public Task<Course> GetCourseAsync(Guid courseId);

        public Task<IEnumerable<Video>> GetCourseVideosAsync(Guid courseId);

        public Task CreateCourseAsync(Course course);

        public Task UpdateCourseAsync(Course course);

        public Task DeleteCourseAsync(Course course);

        public Task<IEnumerable<User>> GetCourseSubscribersAsync(Guid courseId);

        // public Task<IEnumerable<Course>> GetCreatorCoursesAsync(Guid creatorId);

    }
}
