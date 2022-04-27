using Microsoft.AspNetCore.Identity;

namespace WorkoutGlobal.Api.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
