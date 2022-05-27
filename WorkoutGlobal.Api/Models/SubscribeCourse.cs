using WorkoutGlobal.Api.Models.Enums;

namespace WorkoutGlobal.Api.Models
{
    public class SubscribeCourse
    {
        public Guid Id { get; set; }
        public Guid SubscriberId { get; set; }
        public User Subscriber { get; set; }
        public Guid SubscribeCourseId { get; set; }
        public Course Course { get; set; }
        public CourseCompletionRate CourseCompletionRate { get; set; }
        public Guid? LastAvailableVideoId { get; set; }
    }

}
