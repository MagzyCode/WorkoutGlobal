namespace WorkoutGlobal.UI.ViewModels
{
    public class CreationSportEventViewModel
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }

        public Guid TrainerId { get; set; }

        public DateTime EventStartTime { get; set; }
    }
}
