namespace WorkoutGlobal.UI.ApiConnection.Contracts
{
    /// <summary>
    /// Base interface for token worl.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Get created token.
        /// </summary>
        /// <param name="httpContent">Request http context.</param>
        /// <returns>Access token.</returns>
        public string GetToken(HttpContext httpContent);
    }
}
