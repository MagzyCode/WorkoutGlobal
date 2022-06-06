namespace WorkoutGlobal.Api.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        public ICollection<Video> Videos { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<SportEvent> SportEvents { get; set; }
    }
}
