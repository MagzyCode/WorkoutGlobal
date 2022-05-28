namespace WorkoutGlobal.Api.Models.DTOs.SubscribeEventDTOs
{
    public class SubscribeEventDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
    }
}
