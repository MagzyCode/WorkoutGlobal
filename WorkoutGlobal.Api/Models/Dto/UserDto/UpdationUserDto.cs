using WorkoutGlobal.Api.Models.Enums;

namespace WorkoutGlobal.Api.Models.Dto
{
    public class UpdationUserDto
    {
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
        public DateTime DateOfRegistration { get; set; }

        /// <summary>
        /// Trainer official classification number.
        /// </summary>
        public string ClassificationNumber { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
