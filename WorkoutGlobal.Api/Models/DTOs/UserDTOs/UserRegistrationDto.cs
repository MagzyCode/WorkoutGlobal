namespace WorkoutGlobal.Api.Models.DTOs.UserDTOs
{
    /// <summary>
    /// Represents DTO model for user registration.
    /// </summary>
    public class UserRegistrationDto
    {
        /// <summary>
        /// First name of registration user.
        /// </summary>
        /// <example>
        /// Slava
        /// </example>
        public string Name { get; set; }

        /// <summary>
        /// Second name of registration user.
        /// </summary>
        /// <example>
        /// Dranev
        /// </example>
        public string Surname { get; set; }

        /// <summary>
        /// Patronymic of registration user.
        /// </summary>
        /// <example>
        /// Sergeevich
        /// </example>
        public string Patronymic { get; set; }

        /// <summary>
        /// Registration user date of birth.
        /// </summary>
        /// <example>
        /// 07.07.2000 0:00:00
        /// </example>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Country of registration user.
        /// </summary>
        /// <example>
        /// Republic of Belarus
        /// </example>
        public string Country { get; set; }

        /// <summary>
        /// City of registration user.
        /// </summary>
        /// <example>
        /// Gomel
        /// </example>
        public string City { get; set; }

        /// <summary>
        /// Living street of registration user.
        /// </summary>
        /// <example>
        /// Mazurova
        /// </example>
        public string Street { get; set; }

        /// <summary>
        /// Street number of registration user.
        /// </summary>
        /// <example>
        /// 28a
        /// </example>
        public string StreetNumber { get; set; }

        /// <summary>
        /// Room number of registration user.
        /// </summary>
        /// <example>
        /// 1
        /// </example>
        public string RoomNumber { get; set; }
    }
}
