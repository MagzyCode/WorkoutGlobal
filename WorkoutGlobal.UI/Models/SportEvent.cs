namespace WorkoutGlobal.UI.Models
{
    public class SportEvent
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }

        public Guid TrainerId { get; set; }

        public string HostLink { get; set; }

        public string JoinLink { get; set; }
    }
}
