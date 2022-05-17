namespace WorkoutGlobal.Api.Models.DTOs.UserDTOs
{
    /// <summary>
    /// Represents DTO model for user authorization.
    /// </summary>
    public class UserAuthorizationDto
    {
        /// <summary>
        /// User name for authorization.
        /// </summary>
        /// <example>
        /// Anonymous
        /// </example>
        public string UserName { get; set; }

        /// <summary>
        /// User password for authorization
        /// </summary>
        /// <example>
        /// password_1
        /// </example>
        public string Password { get; set; }
    }
}
