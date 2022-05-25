namespace WorkoutGlobal.UI.Models
{
    public class Course
    {
        public Guid Id { get; set; }

        public string CourseName { get; set; }

        public string Description { get; set; }

        public Guid CreatorId { get; set; }

        public byte[] CourseImage { get; set; }
    }
}
