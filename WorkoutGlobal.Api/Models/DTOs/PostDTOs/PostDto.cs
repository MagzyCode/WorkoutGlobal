namespace WorkoutGlobal.Api.Models.DTOs.PostDTOs
{
    public class PostDto
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
        /// Post creation time.
        /// </summary>
        public DateTime PostCreationTime { get; set; }

        /// <summary>
        /// Post context.
        /// </summary>
        public string Context { get; set; }
    }
}
