using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkoutGlobal.Api.Models.Configuration
{
    public class UserAccountsConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User()
                {
                    Id = new Guid("07d1a783-adf7-4dcc-aa35-53abd353152d"),
                    FirstName = "Admin",
                    LastName = "Admin",
                    Patronymic = "Admin",
                    DateOfBirth = new DateTime(1970, 01, 01),
                    ResidencePlace = "Server room",
                    DateOfRegistration = DateTime.UtcNow,
                    UserCredentialsId = "b5b84fd7-5366-44eb-9d1b-408c6a4a8926"
                });
        }
    }
}
