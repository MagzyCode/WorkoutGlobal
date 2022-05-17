using Microsoft.AspNetCore.Identity;

namespace WorkoutGlobal.Api.Models
{
    /// <summary>
    /// Represents account credentials of user.
    /// </summary>
    public class UserCredentials : IdentityUser
    {
        /// <summary>
        /// Salt for user password.
        /// </summary>
        /// <example>
        /// 4bac4486c70fcddb
        /// </example>
        public string PasswordSalt { get; set; }
    }
}
