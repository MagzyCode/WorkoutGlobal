using System.Text.Json;

namespace WorkoutGlobal.Api.Models.ErrorModels
{
    public class ErrorDetails
    {
        /// <summary>
        /// Represents status code of error.
        /// </summary>
        /// <example>
        /// 500
        /// </example>
        public int StatusCode { get; set; }

        /// <summary>
        /// Represents short error message.
        /// </summary>
        /// <example>
        /// Internal server error.
        /// </example>
        public string Message { get; set; }

        /// <summary>
        /// Represents completely described error details.
        /// </summary>
        /// <example>
        /// Ensure that the username and password included in the request are correct.
        /// </example> 
        public string Details { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
