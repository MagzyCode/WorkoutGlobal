using WorkoutGlobal.UI.Models.Enums;

namespace WorkoutGlobal.UI.Models
{
    public class SubscribeCourse
    {
        public Guid Id { get; set; }
        public Guid SubscriberId { get; set; }
        public Guid SubscribeCourseId { get; set; }
        public CourseCompletionRate CourseCompletionRate { get; set; }
        public Guid? LastAvailableVideoId { get; set; }
    }
}
