using System.ComponentModel;

namespace WorkoutGlobal.Api.Models.Enums
{
    /// <summary>
    /// Repsents users sex.
    /// </summary>
    public enum Sex
    {
        [Description("Male sex.")]
        Male,
        [Description("Female sex.")]
        Female
    }
}
