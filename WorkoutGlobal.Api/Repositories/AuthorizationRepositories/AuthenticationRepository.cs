using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.AuthenticationManagerContracts;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.DTOs.UserDTOs;
using WorkoutGlobal.Api.Repositories.BaseRepositories;

namespace WorkoutGlobal.Api.Repositories.AuthorizationRepositories
{
    /// <summary>
    /// Represents authorization manager for log in.
    /// </summary>
    public class AuthenticationRepository : BaseRepository<UserCredentials>, IAuthenticationRepository
    {
        private readonly UserManager<UserCredentials> _userManager;
        private readonly IUserCredentialsRepository _userCredentialsRepository;
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
            IUserCredentialsRepository userCredentialsRepository,
            IMapper mapper) 
            : base(workoutGlobalContext, configuration)
        {
            _userManager = userManager;
            _userCredentialsRepository = userCredentialsRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Create JWT-token for user.
        /// </summary>
        /// <param name="userAuthorizationDto">User credentials.</param>
        /// <returns>JWT-token in string format.</returns>
        public string CreateToken(UserAuthorizationDto userAuthorizationDto)
        {
            var tokenOptions = GenerateTokenOptions(userAuthorizationDto);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        /// <summary>
        /// Find user by his credentials.
        /// </summary>
        /// <param name="userCredentialsDto">User credentials.</param>
        /// <returns>Existed user.</returns>
        public UserCredentials FindUserByCredentials(UserCredentialsDto userCredentialsDto)
        {
            var userCredentials = Context.UserCredentials
                .SingleOrDefault(user => user.UserName == userCredentialsDto.UserName);

            return userCredentials;
        }

        /// <summary>
        /// Generate valid user credentials on registration info.
        /// </summary>
        /// <param name="userCredentialsDto">User credentials.</param>
        public async Task<UserCredentials> GenerateUserCredentialsAsync(UserCredentialsDto userCredentialsDto)
        {
            var userCredentials = _mapper.Map<UserCredentials>(userCredentialsDto);

            var saltBytes = new byte[8];
            new Random().NextBytes(saltBytes);

            var salt = BitConverter.ToString(saltBytes).ToLower().Replace("-", "");

            userCredentials.PasswordSalt = salt;

            userCredentials.PasswordHash = await _userCredentialsRepository.GetHashPasswordAsync(
                password: userCredentialsDto.Password,
                salt: userCredentials.PasswordSalt);

            return userCredentials;
        }

        /// <summary>
        /// Checks is registration use credentials already exists in system.
        /// </summary>
        /// <param name="userRegistrationDto">User registration credentials.</param>
        /// <returns>If user existed in system, return true, otherwise return false.</returns>
        public bool IsUserExisted(UserRegistrationDto userRegistrationDto)
        {
            var userCredentialsDto = _mapper.Map<UserCredentialsDto>(userRegistrationDto);
            var existedUser = FindUserByCredentials(userCredentialsDto);

            return existedUser != null;
        }

        /// <summary>
        /// Registrate user in system.
        /// </summary>
        /// <param name="userCredentials">Registration user credentials.</param>
        /// <returns>A task that represents asynchronous Registrate action.</returns>
        public async Task RegistrateUserAsync(UserCredentials userCredentials)
        {
            await _userManager.CreateAsync(userCredentials);
            await _userManager.AddToRoleAsync(userCredentials, "User");
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

            var userCredentialsDto = _mapper.Map<UserCredentialsDto>(userAuthorizationDto);
            var userCredentials = FindUserByCredentials(userCredentialsDto);

            if (userCredentials == null)
                return false;

            var userPasswordHash = await _userCredentialsRepository.GetHashPasswordAsync(userAuthorizationDto.Password, userCredentials.PasswordSalt);

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
    }

}

