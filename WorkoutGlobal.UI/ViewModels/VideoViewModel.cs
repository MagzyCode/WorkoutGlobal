namespace WorkoutGlobal.UI.ViewModels
{
    public class VideoViewModel
    {
        public Guid Id { get; set; }

        public string Link { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        public bool IsPublic { get; set; }

        public Guid UserId { get; set; }

        public Guid CategoryId { get; set; }
    }
}
