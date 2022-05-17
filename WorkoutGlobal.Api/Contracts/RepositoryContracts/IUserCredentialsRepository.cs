﻿namespace WorkoutGlobal.Api.Contracts.RepositoryContracts
{
    /// <summary>
    /// Base interface for user credentials repository.
    /// </summary>
    public interface IUserCredentialsRepository
    {
        /// <summary>
        /// Create password hash from user password and salt.
        /// </summary>
        /// <param name="password">User salt</param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public Task<string> GetHashPasswordAsync(string password, string salt);
    }
}