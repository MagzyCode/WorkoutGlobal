namespace WorkoutGlobal.UI.ViewModels.Authentication
{
    /// <summary>
    /// Represents view model for authorization user.
    /// </summary>
    public class UserAuthorizationViewModel
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
