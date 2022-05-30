namespace WorkoutGlobal.Api.Models.DTOs.CommentDTOs
{
    public class CreationCommentDto
    {
        public Guid CommentsBlockId { get; set; }
        public Guid CommentatorId { get; set; }
        public string CommentText { get; set; }
        public DateTime PostTime { get; set; } = DateTime.Now;
        public string CommentatorName { get; set; }
    }
}
