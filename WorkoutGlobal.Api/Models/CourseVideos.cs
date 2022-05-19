namespace WorkoutGlobal.Api.Models
{
    /// <summary>
    /// Base model for course videos.
    /// </summary>
    public class CourseVideos
    {
        /// <summary>
        /// Unique identifier of video in course. 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Foreign key of course.
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Foreign model of course.
        /// </summary>
        public Course Course { get; set; }

        /// <summary>
        /// Foreign key of video.
        /// </summary>
        public Guid VideoId { get; set; }

        /// <summary>
        /// Foreign key of video.
        /// </summary>
        public Video Video { get; set; }
    }
}
