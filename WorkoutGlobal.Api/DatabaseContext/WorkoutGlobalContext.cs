using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.DatabaseContext
{
    /// <summary>
    /// Represents project database context.
    /// </summary>
    public class WorkoutGlobalContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Sets database context options.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public WorkoutGlobalContext(DbContextOptions options) : base(options)
        { }
    }
}
