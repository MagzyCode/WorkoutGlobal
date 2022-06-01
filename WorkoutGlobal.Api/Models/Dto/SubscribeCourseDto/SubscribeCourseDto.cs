using WorkoutGlobal.Api.Models.Enums;

namespace WorkoutGlobal.Api.Models.Dto
{
    public class SubscribeCourseDto
    {
        public Guid Id { get; set; }
        public Guid SubscriberId { get; set; }
        public Guid SubscribeCourseId { get; set; }
        public CourseCompletionRate CourseCompletionRate { get; set; }
        public Guid? LastAvailableVideoId { get; set; }
    }
}
