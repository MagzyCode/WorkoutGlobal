using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.Dto;

namespace WorkoutGlobal.Api.Repositories
{
    /// <summary>
    /// Represents authorization manager for log in.
    /// </summary>
    public class AuthenticationRepository : BaseRepository<UserCredentials>, IAuthenticationRepository
    {
        private readonly UserManager<UserCredentials> _userManager;
        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor for authentication repository.
        /// </summary>
        /// <param name="userManager">User manager class instance.</param>
        /// <param name="workoutGlobalContext">Project database context instance.</param>
        /// <param name="configuration">Project configuration instance.</param>
        /// <param name="userCredentialsRepository">User credentials repository instance.</param>
        /// <param name="mapper">AutoMapper instance.</param>
        public AuthenticationRepository(
            UserManager<UserCredentials> userManager, 
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configuration,
            IMapper mapper) 
            : base(workoutGlobalContext, configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Create JWT-token for user.
        /// </summary>
        /// <param name="userAuthorizationDto">User credentials.</param>
        /// <returns>JWT-token in string format.</returns>
        public string CreateToken(UserAuthorizationDto userAuthorizationDto)
        {
            if (userAuthorizationDto == null)
                throw new ArgumentNullException(nameof(userAuthorizationDto));

            var tokenOptions = GenerateTokenOptions(userAuthorizationDto);

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.CanWriteToken
                ? jwtSecurityTokenHandler.WriteToken(tokenOptions)
                : string.Empty;

            return token;
        }

        /// <summary>
        /// Find user by his credentials.
        /// </summary>
        /// <param name="userCredentialsDto">User credentials.</param>
        /// <returns>Existed user.</returns>
        public UserCredentials FindUserByCredentials(UserWithCredentialsDto userCredentialsDto)
        {
            if (_userManager == null)
                throw new ArgumentNullException(nameof(userCredentialsDto));

            var userCredentials = Context.Users
                .Where(user => user.UserName == userCredentialsDto.UserName)
                .SingleOrDefault();

            return userCredentials;
        }

        /// <summary>
        /// Generate valid user credentials on registration info.
        /// </summary>
        /// <param name="userCredentialsDto">User credentials.</param>
        public async Task<UserCredentials> GenerateUserCredentialsAsync(UserWithCredentialsDto userCredentialsDto)
        {
            var userCredentials = _mapper.Map<UserCredentials>(userCredentialsDto);

            var saltBytes = new byte[8];
            new Random().NextBytes(saltBytes);

            userCredentials.PasswordSalt = BitConverter.ToString(saltBytes).ToLower().Replace("-", ""); ;
            userCredentials.PasswordHash = await GenerateHashPasswordAsync(userCredentialsDto.Password, userCredentials.PasswordSalt);

            return userCredentials;
        }

        /// <summary>
        /// Checks is registration use credentials already exists in system.
        /// </summary>
        /// <param name="userRegistrationDto">User registration credentials.</param>
        /// <returns>If user existed in system, return true, otherwise return false.</returns>
        public bool IsUserExisted(UserRegistrationDto userRegistrationDto)
        {
            if (userRegistrationDto == null)
                throw new ArgumentNullException(nameof(userRegistrationDto));

            var userCredentialsDto = _mapper.Map<UserWithCredentialsDto>(userRegistrationDto);
            var existedUser = FindUserByCredentials(userCredentialsDto);

            return existedUser != null;
        }

        /// <summary>
        /// Registrate user in system.
        /// </summary>
        /// <param name="userCredentials">Registration user credentials.</param>
        /// <returns>A task that represents asynchronous Registrate action.</returns>
        public async Task RegistrateUserAsync(UserRegistrationDto userRegistrationDto)
        {
            if (userRegistrationDto == null)
                throw new ArgumentNullException(nameof(userRegistrationDto));

            var userCredentialsDto = _mapper.Map<UserWithCredentialsDto>(userRegistrationDto);
            var userCredentials = await GenerateUserCredentialsAsync(userCredentialsDto);
            var user = _mapper.Map<User>(userRegistrationDto);

            await _userManager.CreateAsync(userCredentials);
            user.UserCredentialsId = userCredentials.Id;
            await _userManager.AddToRoleAsync(userCredentials, "User");

            await Context.UserAccounts.AddAsync(user);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks the user's data when log in.
        /// </summary>
        /// <param name="userAuthorizationDto">User credentials.</param>
        /// <returns>If user credentials exists in system, return true,
        /// otherwise return false.</returns>
        public async Task<bool> ValidateUserAsync(UserAuthorizationDto userAuthorizationDto)
        {
            if (userAuthorizationDto == null)
                return false;

            var userCredentialsDto = _mapper.Map<UserWithCredentialsDto>(userAuthorizationDto);
            var userCredentials = FindUserByCredentials(userCredentialsDto);

            if (userCredentials == null)
                return false;

            var userPasswordHash = await GenerateHashPasswordAsync(userAuthorizationDto.Password, userCredentials.PasswordSalt);

            return userCredentials != null 
                && userCredentials.PasswordHash == userPasswordHash;
        }

        /// <summary>
        /// Generate token options.
        /// </summary>
        /// <param name="userAuthorizationDto">User credentials.</param>
        /// <returns>Returns JwtSecurityToken instanse.</returns>
        private JwtSecurityToken GenerateTokenOptions(UserAuthorizationDto userAuthorizationDto)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            
            var signingCredentials = new SigningCredentials(
                key: new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.GetSection("Key").Value)),
                algorithm: SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userAuthorizationDto.UserName)
            };

            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings.GetSection("ValidIssuer").Value,
                audience: jwtSettings.GetSection("ValidAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("Expires").Value)),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }

        private async Task<string> GenerateHashPasswordAsync(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = await sha256.ComputeHashAsync(
                inputStream: new MemoryStream(Encoding.UTF8.GetBytes(password + salt)));

            var hashPassword = BitConverter.ToString(hashedBytes).ToString().ToLower().Replace("-", "");

            return hashPassword;
        }
    }

}

