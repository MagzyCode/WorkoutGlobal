using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.DatabaseContext
{
    public class WorkoutGlobalContext : IdentityDbContext<User>
    {
        public WorkoutGlobalContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
