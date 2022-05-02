using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkoutGlobal.Api.Models.Configuration
{
    /// <summary>
    /// Seeding database with initial roles for users.
    /// </summary>
    public class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        /// <summary>
        /// Seeding database with initial admin role for user.
        /// </summary>
        /// <param name="builder">Model builder.</param>
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>()
                {
                    UserId = "07ca8c3c-2a1b-4423-98fd-f4bb8359feb8",
                    RoleId = "6abe6f33-ae4b-4430-8f14-493dc9a5a9d1"
                });
        }
    }
}
