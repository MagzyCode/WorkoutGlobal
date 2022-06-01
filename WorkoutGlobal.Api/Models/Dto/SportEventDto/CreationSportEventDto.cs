namespace WorkoutGlobal.Api.Models.Dto
{
    public class CreationSportEventDto
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }

        public Guid TrainerId { get; set; }

        public string HostLink { get; set; }

        public string JoinLink { get; set; }
    }
}
