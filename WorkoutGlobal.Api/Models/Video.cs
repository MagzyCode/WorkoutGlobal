﻿namespace WorkoutGlobal.Api.Models
{
    /// <summary>
    /// Base model for video.
    /// </summary>
    public class Video
    {
        /// <summary>
        /// Video unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Link on user video.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Represents video openness in system.
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Foreign key with user account.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Foreign model with user account.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Collection of video in courses.
        /// </summary>
        public ICollection<CourseVideos> VideoCourses { get; set; }
    }
}