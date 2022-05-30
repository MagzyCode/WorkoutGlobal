namespace WorkoutGlobal.UI.ViewModels
{
    public class VideoWithCommentsAndSubscriptionViewModel
    {
        /// <summary>
        /// Video unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Link on user video.
        /// </summary>
        public string Link { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Represents video openness in system.
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Foreign key with user account.
        /// </summary>
        public Guid UserId { get; set; }

        public List<CommentViewModel> Comments { get; set; }
        public string AdditionComment { get; set; }

        public Guid SubscriberId { get; set; }

        public bool IsSubscribe { get; set; }
    }
}
