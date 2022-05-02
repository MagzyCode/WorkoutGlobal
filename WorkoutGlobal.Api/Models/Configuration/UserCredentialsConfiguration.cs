using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkoutGlobal.Api.Models.Configuration
{
    /// <summary>
    /// Represent seeding of user credentials.
    /// </summary>
    public class UserCredentialsConfiguration : IEntityTypeConfiguration<UserCredentials>
    {
        /// <summary>
        /// Seeding database with initial user credentials.
        /// </summary>
        /// <param name="builder">Model builder</param>
        public void Configure(EntityTypeBuilder<UserCredentials> builder)
        {
            builder.HasData(
                new UserCredentials()
                {
                    Id = new Guid("b5b84fd7-5366-44eb-9d1b-408c6a4a8926"),
                    Password = "qwerty123",
                    UserId = "07ca8c3c-2a1b-4423-98fd-f4bb8359feb8"
                });
        }
    }
}
