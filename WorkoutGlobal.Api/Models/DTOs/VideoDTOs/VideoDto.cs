﻿namespace WorkoutGlobal.Api.Models.DTOs.VideoDTOs
{
    /// <summary>
    /// Video DTO model.
    /// </summary>
    public class VideoDto
    {
        /// <summary>
        /// Video unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Link on user video.
        /// </summary>
        public string Link { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Represents video openness in system.
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Foreign key with user account.
        /// </summary>
        public Guid UserId { get; set; }
    }
}
