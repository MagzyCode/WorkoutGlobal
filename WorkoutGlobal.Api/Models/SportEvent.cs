namespace WorkoutGlobal.Api.Models
{
    public class SportEvent
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }

        public Guid TrainerId { get; set; }

        public User EventCreator { get; set; }

        public string HostLinl { get; set; }

        public string JoinLink { get; set; }
        
        public ICollection<SubscribeEvent> ParticipatingUsers { get; set; }
    }
}
