using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Repositories
{
    public class SubscribeCourseRepository : BaseRepository<SubscribeCourse>, ISubscribeCourseRepository
    {
        public SubscribeCourseRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager)
            : base(workoutGlobalContext, configurationManager)
        { }

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

        public async Task<SubscribeCourse> GetSubscribeCourseAsync(Guid subscribeCourseId)
        {
            var model = await GetModelAsync(subscribeCourseId);

            return model;
        }

        public async Task<bool> IsCourseSubscriptionExists(Guid userId, Guid courseId)
        {
            var isExisted = await Context.SubscribeCourses.AnyAsync(model => model.SubscriberId == userId && model.SubscribeCourseId == courseId);

            return isExisted;
        }

        public async Task UpdateSubscribeCourseAsync(SubscribeCourse subscribeCourse)
        {
            Update(subscribeCourse);
            await SaveChangesAsync();
        }
    }
}
