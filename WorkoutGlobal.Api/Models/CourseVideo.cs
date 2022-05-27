namespace WorkoutGlobal.Api.Models
{
    /// <summary>
    /// Base model for course videos.
    /// </summary>
    public class CourseVideo
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

        /// <summary>
        /// Identify video position in course sequence of video. Starts with 1.  
        /// </summary>
        public int SequenceNumber { get; set; }
    }
}
