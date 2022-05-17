﻿namespace WorkoutGlobal.UI.ViewModels.Authentication
{
    /// <summary>
    /// View model of registration user.
    /// </summary>
    public class UserRegistrationViewModel
    {
        /// <summary>
        /// User name for registration.
        /// </summary>
        /// <example>
        /// Anonymous
        /// </example>
        public string UserName { get; set; }

        /// <summary>
        /// User email for registration.
        /// </summary>
        /// <example>
        /// aaaaaa@mail.com
        /// </example>
        public string Email { get; set; }

        /// <summary>
        /// User password for registration.
        /// </summary>
        /// <example>
        /// password_1
        /// </example>
        public string Password { get; set; }

        /// <summary>
        /// User confirm password for registration.
        /// </summary>
        /// <example>
        /// password_1
        /// </example>
        public string ConfirmPassword { get; set; }
    }
}