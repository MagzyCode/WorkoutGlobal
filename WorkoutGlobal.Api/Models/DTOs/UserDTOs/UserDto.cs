using WorkoutGlobal.Api.Models.Enums;

namespace WorkoutGlobal.Api.Models.DTOs.UserDTOs
{
    public class UserDto
    {
        /// <summary>
        /// User account unique identifier. 
        /// </summary>
        public Guid Id { get; set; }

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

        /// <summary>
        /// Identifies whether the trainer's status has been confirmed.
        /// </summary>
        public bool IsStatusVerify { get; set; }

        /// <summary>
        /// Foreign key with user credentials model.
        /// </summary>
        public string UserCredentialsId { get; set; }
    }
}
