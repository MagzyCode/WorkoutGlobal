namespace WorkoutGlobal.Api.Models.DTOs.UserDTOs
{
    /// <summary>
    /// Represents base user credentials dto.
    /// </summary>
    public class UserCredentialsDto
    {
        /// <summary>
        /// User name.
        /// </summary>
        /// <example>
        /// Anonymous
        /// </example>
        public string UserName { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        /// <example>
        /// password_1
        /// </example>
        public string Password { get; set; }

        /// <summary>
        /// User email for registration.
        /// </summary>
        /// <example>
        /// aaaaaa@mail.com
        /// </example>
        public string Email { get; set; }
    }
}
