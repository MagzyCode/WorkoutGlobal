using System.ComponentModel;

namespace WorkoutGlobal.Api.Models.Enums
{
    /// <summary>
    /// Represents user sport activity in life.
    /// </summary>
    public enum SportsActivity
    {
        [Description("Very active user with many sports activities.")]
        Athletic,
        [Description("Active user with few sports activities.")]
        Active,
        [Description("User have moderate activities in life such as walking, cycling and etc.")]
        Moderate,
        [Description("User have reduces activies.")]
        Reduced,
        [Description("Very inactive person position.")]
        Inactive
    }
}
