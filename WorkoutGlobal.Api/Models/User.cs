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
        public string Name { get; set; }

        /// <summary>
        /// Last name of user.
        /// </summary>
        /// <example>
        /// Bezzos
        /// </example>
        public string Surname { get; set; }

        /// <summary>
        /// Patronymic of user.
        /// </summary>
        /// <example>
        /// Sergeevich
        /// </example>
        public string Patronymic { get; set; }

        /// <summary>
        /// Date of birth of user.
        /// </summary>
        /// <example>
        /// 20.07.2015 0:00:00
        /// </example>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// User place of living.
        /// </summary>
        /// <example>
        /// Republic of Belarus, Minsk, Sovetskaya Street, 28/25
        /// </example>
        public string ResidentPlace { get; set; }

        /// <summary>
        /// Classification book number of trainer.
        /// </summary>
        /// <example>
        /// 1234567891023456
        /// </example>
        public string ClassificationBookNumber { get; set; }

        /// <summary>
        /// Date and time of registration user in system.
        /// </summary>
        /// <example>
        /// 20.07.2015 0:00:00
        /// </example>
        public DateTime DateOfRegistration { get; set; }

        /// <summary>
        /// Represents user credentials in account.
        /// </summary>
        public UserCredentials UserCredentials { get; set; }
    }
}
