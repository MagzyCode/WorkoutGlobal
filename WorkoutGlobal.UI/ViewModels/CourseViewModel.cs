namespace WorkoutGlobal.UI.ViewModels
{
    public class CourseViewModel
    {
        public Guid Id { get; set; }

        public string CourseName { get; set; }

        public string Description { get; set; }

        public Guid CreatorId { get; set; }

        public IFormFile CourseImageForm { get; set; }

        public byte[] CourseImage { get; set; }

        public Guid CategoryId { get; set; }
    }
}
