namespace WorkoutGlobal.UI.Models
{
    /// <summary>
    /// Base error view model.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Id of request.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Show if request id exists.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}