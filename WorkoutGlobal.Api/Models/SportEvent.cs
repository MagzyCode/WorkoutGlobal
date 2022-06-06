namespace WorkoutGlobal.Api.Models
{
    public class SportEvent
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }

        public Guid TrainerId { get; set; }

        public User EventCreator { get; set; }

        public string HostLink { get; set; }

        public string JoinLink { get; set; }

        public DateTime EventStartTime { get; set; }
        
        public ICollection<SubscribeEvent> ParticipatingUsers { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
