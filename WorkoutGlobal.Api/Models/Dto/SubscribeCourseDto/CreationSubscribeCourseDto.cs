using WorkoutGlobal.Api.Models.Enums;

namespace WorkoutGlobal.Api.Models.Dto
{
    public class CreationSubscribeCourseDto
    {
        public Guid SubscriberId { get; set; }
        public Guid SubscribeCourseId { get; set; }
        public CourseCompletionRate CourseCompletionRate { get; set; }
        public Guid? LastAvailableVideoId { get; set; }
    }
}
