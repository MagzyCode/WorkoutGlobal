namespace WorkoutGlobal.Api.Models
{
    /// <summary>
    /// Base model for trainers courses.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Course unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of course.
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// Course description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Foreign key with user account.
        /// </summary>
        public Guid CreatorId { get; set; }

        /// <summary>
        /// Foreign model with user account.
        /// </summary>
        public User Creator { get; set; }

        public byte[] CourseImage { get; set; }

        /// <summary>
        /// Collection of video in course.
        /// </summary>
        public ICollection<CourseVideos> CourseVideos { get; set; }
    }
}
