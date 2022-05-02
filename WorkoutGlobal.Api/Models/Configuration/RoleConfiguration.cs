using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkoutGlobal.Api.Models.Configuration
{
    /// <summary>
    /// Represent seeding of roles.
    /// </summary>
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        /// <summary>
        /// Seeding database with initial roles values.
        /// </summary>
        /// <param name="builder">Model builder.</param>
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                { 
                    Id = "f4a4ce79-c6b3-4e12-9c98-ff07b5030752",
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Id = "6abe6f33-ae4b-4430-8f14-493dc9a5a9d1",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
           );
        }
    }
}
