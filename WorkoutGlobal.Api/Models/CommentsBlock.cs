namespace WorkoutGlobal.Api.Models
{
    public class CommentsBlock
    {
        public Guid Id { get; set; }
        public Guid CommentedVideoId { get; set; }
        public Video CommentedVideo { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
