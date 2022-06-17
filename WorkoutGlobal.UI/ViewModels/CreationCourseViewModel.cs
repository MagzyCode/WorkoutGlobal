namespace WorkoutGlobal.UI.ViewModels
{
    public class CreationCourseViewModel
    {
        public Guid Id { get; set; }

        public string CourseName { get; set; }

        public string Description { get; set; }

        public Guid CreatorId { get; set; }

        public IFormFile CourseImageForm { get; set; }

        public byte[] CourseImage { get; set; }

        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        public List<string> Categories { get; set; }

        public List<(Guid videoId, string videoTitle)> CourseVideos { get; set; }

        public List<string> SelectedVideos { get; set; }
    }
}
