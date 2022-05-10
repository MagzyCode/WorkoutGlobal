using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.Configuration;

namespace WorkoutGlobal.Api.Context
{
    /// <summary>
    /// Represents context of WorkoutGlobal project.
    /// </summary>
    public class WorkoutGlobalContext : IdentityDbContext<UserCredentials>
    {
        /// <summary>
        /// Ctor for set context options.
        /// </summary>
        /// <param name="options">Context options.</param>
        public WorkoutGlobalContext(DbContextOptions options)
            : base(options)
        { }

        /// <summary>
        /// Configure the scheme needed for the identity framework.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserCredentialsConfiguration());
            modelBuilder.ApplyConfiguration(new UserRolesConfiguration());
        }

        /// <summary>
        /// Represents table of user credentials.
        /// </summary>
        public DbSet<UserCredentials> UserCredentials { get; set; }
    }
}
