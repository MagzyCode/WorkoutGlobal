namespace WorkoutGlobal.Api.Models.DTOs.StoreVideoDTOs
{
    public class StoreVideoDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid SavedVideoId { get; set; }
    }
}
