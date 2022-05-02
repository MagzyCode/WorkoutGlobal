using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkoutGlobal.Api.Models.Configuration
{
    /// <summary>
    /// Seeding database with initial admin in system.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Seeding database with initial admin profile.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User()
                {
                    Id = "07ca8c3c-2a1b-4423-98fd-f4bb8359feb8",
                    Name = "Mikhail",
                    Surname = "Kazarevich",
                    Patronymic = "Andreevich",
                    DateOfBirth = DateTime.Parse("21/09/2000 16:15:00"),
                    ResidentPlace = "Republic of Belarus, Minsk, Sovetskaya Street, 28/25",
                    ClassificationBookNumber = null,
                    DateOfRegistration = DateTime.Now
                }
            );
        }
    }
}
