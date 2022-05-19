namespace WorkoutGlobal.Api.Models
{
    /// <summary>
    /// Base model for user posts.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Unique identifier of post.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Foreign key for creator.
        /// </summary>
        public Guid CreatorId { get; set; }

        /// <summary>
        /// Foreign model for creator.
        /// </summary>
        public User Creator { get; set; }

        /// <summary>
        /// Post creation time.
        /// </summary>
        public DateTime PostCreationTime { get; set; }

        /// <summary>
        /// Post context.
        /// </summary>
        public string Context { get; set; }
    }
}
