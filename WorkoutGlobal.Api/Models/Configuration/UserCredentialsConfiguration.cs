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
                    Id = "b5b84fd7-5366-44eb-9d1b-408c6a4a8926",
                    UserName = "MagzyCode",
                    PasswordSalt = "46da4fb783d806ab",
                    PasswordHash = "21c9b9e74e5071de6d6c872ccae5af4deb3b42563cd649a3179a5780163b6238"
                });
        }
    }
}
