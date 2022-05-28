using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Repositories.BaseRepositories;

namespace WorkoutGlobal.Api.Repositories.ModelsRepositories
{
    public class SubscribeCourseRepository : BaseRepository<SubscribeCourse>, ISubscribeCourseRepository
    {
        //private readonly ICourseRepository _courseRepository;
        // private readonly IUserRepository _userRepository;

        public SubscribeCourseRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager)
            // ICourseRepository courseRepository,
            //IUserRepository userRepository) 
            : base(workoutGlobalContext, configurationManager)
        {
            // _courseRepository = courseRepository;
            // _userRepository = userRepository;
        }

        public async Task CreateSubscribeCourseAsync(SubscribeCourse subscribeCourse)
        {
            await CreateAsync(subscribeCourse);
            await SaveChangesAsync();
        }

        public async Task DeleteSubscribeCourseAsync(SubscribeCourse subscribeCourseId)
        {
            Delete(subscribeCourseId);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<SubscribeCourse>> GetAllSubscribeCourseAsync()
        {
            var models = await GetAll().ToListAsync();

            return models;
        }

        //public async Task<IEnumerable<User>> GetCourseSubscribersAsync(Guid courseId)
        //{
        //    var usersIds = await GetAll()
        //        .Where(x => x.SubscribeCourseId == courseId)
        //        .Select(x => x.SubscriberId)
        //        .ToListAsync();

        //    var users = new List<User>();

        //    foreach (var userId in usersIds)
        //        users.Add(await _userRepository.GetUserAsync(userId));

        //    return users;
        //}

        public async Task<SubscribeCourse> GetSubscribeCourseAsync(Guid subscribeCourseId)
        {
            var model = await GetModelAsync(subscribeCourseId);

            return model;
        }

        //public async Task<IEnumerable<Course>> GetUserSubscribeCoursesAsync(Guid userId)
        //{
        //    var coursesIds = await GetAll()
        //        .Where(x => x.SubscriberId == userId)
        //        .Select(x => x.SubscribeCourseId)
        //        .ToListAsync();

        //    var courses = new List<Course>();

        //    foreach (var courseId in coursesIds)
        //        courses.Add(await Context.Courses.FindAsync(courseId));

        //    return courses;
        //}

        public async Task UpdateSubscribeCourseAsync(SubscribeCourse subscribeCourse)
        {
            Update(subscribeCourse);
            await SaveChangesAsync();
        }
    }
}
