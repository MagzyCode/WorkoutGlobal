namespace WorkoutGlobal.Api.Models.DTOs.CourseVideoDTOs
{
    public class CreationCourseVideoDto
    {
        /// <summary>
        /// Foreign key of course.
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Foreign key of video.
        /// </summary>
        public Guid VideoId { get; set; }

        /// <summary>
        /// Identify video position in course sequence of video. Starts with 1.  
        /// </summary>
        public int SequenceNumber { get; set; }
    }
}
