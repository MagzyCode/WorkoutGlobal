using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkoutGlobal.Api.Models.Configuration
{
    /// <summary>
    /// Represent seeding of user roles.
    /// </summary>
    public class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        /// <summary>
        /// Seeding database with initial user roles values.
        /// </summary>
        /// <param name="builder">Model builder.</param>
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    UserId = "b5b84fd7-5366-44eb-9d1b-408c6a4a8926",
                    RoleId = "6abe6f33-ae4b-4430-8f14-493dc9a5a9d1"
                }
           );
        }
    }
}
