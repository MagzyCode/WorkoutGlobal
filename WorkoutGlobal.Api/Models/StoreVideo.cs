namespace WorkoutGlobal.Api.Models
{
    public class StoreVideo
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid SavedVideoId { get; set; }

        public Video SavedVideo { get; set; }
    }
}
