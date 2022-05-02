using System.ComponentModel.DataAnnotations;

namespace WorkoutGlobal.Api.Models
{
    /// <summary>
    /// Represents account credentials of user.
    /// </summary>
    public class UserCredentials
    {
        /// <summary>
        /// User credentials unique identifier.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Password of user in system.
        /// </summary>
        /// <example>
        /// qwerty123
        /// </example>
        public string Password { get; set; }

        /// <summary>
        /// Account profile image.
        /// </summary>
        public byte[] AccountImage { get; set; }

        /// <summary>
        /// Foreign key with User model.
        /// </summary>
        /// <example>
        /// f58ac57f-d492-4e83-a86a-650609ff39bf
        /// </example>
        public string UserId { get; set; }

        /// <summary>
        /// Navigation property for User model.
        /// </summary>
        public User User { get; set; }
    }
}
