using WorkoutGlobal.UI.Models.Enums;

namespace WorkoutGlobal.UI.ViewModels
{
    public class SubscribeCourseViewModel
    {
        public Guid Id { get; set; }
        public Guid SubscriberId { get; set; }
        public Guid SubscribeCourseId { get; set; }
        public CourseCompletionRate CourseCompletionRate { get; set; }
        public Guid? LastAvailableVideoId { get; set; }
    }
}
