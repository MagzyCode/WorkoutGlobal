using System.Security.Cryptography;
using System.Text;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;

namespace WorkoutGlobal.Api.Repositories.ModelsRepositories
{
    /// <summary>
    /// Repository for main user credential actions.
    /// </summary>
    public class UserCredentialsRepository : IUserCredentialsRepository
    {
        /// <summary>
        /// Get user hashed password by real password and existed password salt.
        /// </summary>
        /// <param name="password">User password.</param>
        /// <param name="salt">User pasword salt.</param>
        /// <returns>Password hash.</returns>
        public async Task<string> GetHashPasswordAsync(string password, string salt)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            if (string.IsNullOrEmpty(salt))
                throw new ArgumentNullException(nameof(salt));

            using var sha256 = SHA256.Create();
            var hashedBytes = await sha256.ComputeHashAsync(
                inputStream : new MemoryStream(Encoding.UTF8.GetBytes(password + salt)));

            var hashPassword = BitConverter.ToString(hashedBytes).ToString().ToLower().Replace("-", "");

            return hashPassword;
        }
    }
}
