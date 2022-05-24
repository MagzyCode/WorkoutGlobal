namespace WorkoutGlobal.Api.Models.DTOs.CommentDTOs
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public Guid CommentsBlockId { get; set; }
        public Guid CommentatorId { get; set; }
        public string CommentText { get; set; }
        public DateTime PostTime { get; set; } = DateTime.Now;
    }
}
