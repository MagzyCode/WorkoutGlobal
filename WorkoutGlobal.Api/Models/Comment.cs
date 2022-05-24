namespace WorkoutGlobal.Api.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid CommentsBlockId { get; set; }
        public CommentsBlock CommentsBlock { get; set; }
        public Guid CommentatorId { get; set; }
        public User Commentator { get; set; }
        public string CommentText { get; set; }
        public DateTime PostTime { get; set; } = DateTime.Now;
    }
}
