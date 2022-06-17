using WorkoutGlobal.UI.Models.Enums;

namespace WorkoutGlobal.UI.ViewModels
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

        /// <summary>
        /// User first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User patronymic.
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// User date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// User residence place.
        /// </summary>
        public string ResidencePlace { get; set; }

        /// <summary>
        /// User sex.
        /// </summary>
        public Sex Sex { get; set; }

        /// <summary>
        /// User height.
        /// </summary>
        public double? Height { get; set; }

        /// <summary>
        /// User weight.
        /// </summary>
        public double? Weight { get; set; }

        /// <summary>
        /// User's attitude to sports.
        /// </summary>
        public SportsActivity SportsActivity { get; set; }


        /// <summary>
        /// Registration date for user in system. 
        /// </summary>
        public DateTime DateOfRegistration { get; set; } = DateTime.Now;

        /// <summary>
        /// Trainer official classification number.
        /// </summary>
        public string ClassificationNumber { get; set; }
    }
}
