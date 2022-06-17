namespace WorkoutGlobal.UI.ViewModels
{
    public class CreationVideoViewModel
    {
        public string Link { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public Guid UserId { get; set; }

        public string CategoryName { get; set; }

        public List<string> Categories { get; set; }
    }
}
