namespace WorkoutGlobal.Api.Models.DTOs.CourseDTOs
{
    public class CreationCourseDto
    {
        public string CourseName { get; set; }

        public string Description { get; set; }

        public Guid CreatorId { get; set; }

        public byte[] CourseImage { get; set; }

        public Guid CategoryId { get; set; }
    }
}
