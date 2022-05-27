namespace WorkoutGlobal.Api.Models
{
    public class SubscribeEvent
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid EventId { get; set; }
        public SportEvent Event { get; set; } 
    }
}
