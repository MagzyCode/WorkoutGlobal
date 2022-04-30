using Microsoft.AspNetCore.Identity;

namespace WorkoutGlobal.Api.Models
{
    /// <summary>
    /// Base model for all users in system.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// First name of user.
        /// </summary>
        /// <example>
        /// John
        /// </example>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of user.
        /// </summary>
        /// <example>
        /// Bezzos
        /// </example>
        public string LastName { get; set; }
    }
}
