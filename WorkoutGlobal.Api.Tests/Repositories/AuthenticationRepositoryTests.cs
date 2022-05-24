using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Threading.Tasks;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.DTOs.UserDTOs;
using WorkoutGlobal.Api.Repositories.AuthorizationRepositories;
using WorkoutGlobal.Api.Tests.Configuration;
using Xunit;

namespace WorkoutGlobal.Api.Tests.Repositories
{
    public class AuthenticationRepositoryTests
    {
        private readonly IConfiguration _testConfiguration;
        private readonly AuthenticationRepository _authenticationRepository;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IUserCredentialsRepository> _userCredentialsRepository;
        private readonly Mock<IConfigurationSection> _jwtSettings;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public AuthenticationRepositoryTests()
        {
            _testConfiguration = ConfigurationAccessor.GetTestConfiguration();
            _userRepositoryMock = new Mock<IUserRepository>();
            _jwtSettings = new Mock<IConfigurationSection>();
            _configuration = new Mock<IConfiguration>();
            _configuration
                .Setup(x => x.GetSection("JwtSettings"))
                .Returns(_jwtSettings.Object);

            _mapper = new Mock<IMapper>();
            _mapper
                .Setup(x => x.Map<UserCredentials>(It.IsAny<UserCredentialsDto>()))
                .Returns(new UserCredentials());

            _userCredentialsRepository = new Mock<IUserCredentialsRepository>();
            _userCredentialsRepository
                .Setup(x => x.GetHashPasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(string.Empty));
            _userCredentialsRepository
                .Setup(x => x.GetHashPasswordAsync(null, It.IsAny<string>()))
                .Throws(() => new ArgumentNullException());

            _authenticationRepository = new AuthenticationRepository(
                null,
                null,
                _configuration.Object,
                _userCredentialsRepository.Object,
                _mapper.Object,
                _userRepositoryMock.Object);
        }

        [Fact]
        public void CreateToken_NullArgument_ReturnArgumentNullException()
        {
            // arrange
            UserAuthorizationDto? userAuthorizationDto = null;

            // act
            var result = () => _authenticationRepository.CreateToken(userAuthorizationDto);

            // assert
            result.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CreateToken_ValidUserCredentials_ReturnValidJWTToken()
        {
            // arrange
            var userAuthorizationDto = new UserAuthorizationDto()
            {
                UserName = "Mikhail111",
                Password = "qwerty123"
            };

            _jwtSettings
                .Setup(x => x.GetSection("Key").Value)
                .Returns(_testConfiguration["JwtSettingsValid:Key"]);
            _jwtSettings
                .Setup(x => x.GetSection("ValidIssuer").Value)
                .Returns(_testConfiguration["JwtSettingsValid:ValidIssuer"]);
            _jwtSettings
                .Setup(x => x.GetSection("ValidAudience").Value)
                .Returns(_testConfiguration["JwtSettingsValid:ValidAudience"]);
            _jwtSettings
                .Setup(x => x.GetSection("Expires").Value)
                .Returns(_testConfiguration["JwtSettingsValid:Expires"]);

            // act
            var result = _authenticationRepository.CreateToken(userAuthorizationDto);

            // assert
            result.Should().BeOfType<string>();
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().MatchEquivalentOf("*.*.*");
        }

        [Fact]
        public void CreateToken_InvalidKeyLength_ReturnArgumentOutOfRangeException()
        {
            // arrange
            var userAuthorizationDto = new UserAuthorizationDto()
            {
                UserName = "Mikhail111",
                Password = "qwerty123"
            };

            _jwtSettings
                .Setup(x => x.GetSection("Key").Value)
                .Returns(_testConfiguration["JwtSettingsInvalidKeyLength:Key"]);
            _jwtSettings
                .Setup(x => x.GetSection("ValidIssuer").Value)
                .Returns(_testConfiguration["JwtSettingsInvalidKeyLength:ValidIssuer"]);
            _jwtSettings
                .Setup(x => x.GetSection("ValidAudience").Value)
                .Returns(_testConfiguration["JwtSettingsInvalidKeyLength:ValidAudience"]);
            _jwtSettings
                .Setup(x => x.GetSection("Expires").Value)
                .Returns(_testConfiguration["JwtSettingsInvalidKeyLength:Expires"]);

            // act
            var result = () => _authenticationRepository.CreateToken(userAuthorizationDto);

            // assert
            result.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void CreateToken_InvalidUserName_ReturnArgumentNullException()
        {
            // arrange
            var userAuthorizationDto = new UserAuthorizationDto()
            {
                UserName = null,
                Password = null
            };

            _jwtSettings
                .Setup(x => x.GetSection("Key").Value)
                .Returns(_testConfiguration["JwtSettingsValid:Key"]);

            // act
            var result = () => _authenticationRepository.CreateToken(userAuthorizationDto);

            // assert
            result.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CreateToken_InvalidExpires_ReturnFormatException()
        {
            // arrange
            var userAuthorizationDto = new UserAuthorizationDto()
            {
                UserName = "Mikhail111",
                Password = "qwerty123"
            };

            _jwtSettings
                .Setup(x => x.GetSection("Key").Value)
                .Returns(_testConfiguration["JwtSettingsInvalidExpires:Key"]);
            _jwtSettings
                .Setup(x => x.GetSection("ValidIssuer").Value)
                .Returns(_testConfiguration["JwtSettingsInvalidExpires:ValidIssuer"]);
            _jwtSettings
                .Setup(x => x.GetSection("ValidAudience").Value)
                .Returns(_testConfiguration["JwtSettingsInvalidExpires:ValidAudience"]);
            _jwtSettings
                .Setup(x => x.GetSection("Expires").Value)
                .Returns(_testConfiguration["JwtSettingsInvalidExpires:Expires"]);

            // act
            var result = () => _authenticationRepository.CreateToken(userAuthorizationDto);

            // assert
            result.Should().Throw<FormatException>();
        }

        [Fact]
        public void CreateToken_InvalidPassword_ReturnJWTToken()
        {
            // arrange
            var userAuthorizationDto = new UserAuthorizationDto()
            {
                UserName = "Mikhail111",
                Password = null
            };

            _jwtSettings
                .Setup(x => x.GetSection("Key").Value)
                .Returns(_testConfiguration["JwtSettingsValid:Key"]);
            _jwtSettings
                .Setup(x => x.GetSection("ValidIssuer").Value)
                .Returns(_testConfiguration["JwtSettingsValid:ValidIssuer"]);
            _jwtSettings
                .Setup(x => x.GetSection("ValidAudience").Value)
                .Returns(_testConfiguration["JwtSettingsValid:ValidAudience"]);
            _jwtSettings
                .Setup(x => x.GetSection("Expires").Value)
                .Returns(_testConfiguration["JwtSettingsValid:Expires"]);

            // act
            var result = _authenticationRepository.CreateToken(userAuthorizationDto);

            // assert
            result.Should().BeOfType<string>();
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().MatchEquivalentOf("*.*.*");
        }

        [Fact]
        public async Task GenerateUserCredentialsAsync_NullUserCredentials_ReturnNullReferenceException()
        {
            // act
            UserCredentialsDto? userCredentialsDto = null;

            // arrange
            var result = async () => await _authenticationRepository.GenerateUserCredentialsAsync(userCredentialsDto);

            // assert
            await result.Should().ThrowAsync<NullReferenceException>();
        }

        [Fact]
        public async Task GenerateUserCredentialsAsync_NullUserPassword_ReturnArgumentNullException()
        {
            // act
            var userCredentialsDto = new UserCredentialsDto()
            {
                UserName = string.Empty,
                Email = string.Empty,
                Password = null
            };

            // arrange
            var result = async () => await _authenticationRepository.GenerateUserCredentialsAsync(userCredentialsDto);

            // assert
            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GenerateUserCredentialsAsync_NullUserPassword_ReturnUserPasswordHash()
        {
            // act
            var userCredentialsDto = new UserCredentialsDto()
            {
                UserName = null,
                Email = null,
                Password = "qwerty123"
            };

            // arrange
            var result = await _authenticationRepository.GenerateUserCredentialsAsync(userCredentialsDto);
          
            result.Should().BeOfType<UserCredentials>();
            result.PasswordHash.Should().NotBeNull();
        }

        [Fact]
        public async Task GenerateUserCredentialsAsync_ValidUserPassword_ReturnUserPasswordHash()
        {
            // act
            var userCredentialsDto = new UserCredentialsDto()
            {
                UserName = "",
                Email = "",
                Password = "qwerty123"
            };

            // arrange
            var result = await _authenticationRepository.GenerateUserCredentialsAsync(userCredentialsDto);

            // assert
            result.Should().BeOfType<UserCredentials>();
            result.PasswordHash.Should().NotBeNull();
        }

        [Fact]
        public void IsUserExisted_NullUserRegistration_ReturnNullArgumentException()
        {
            // arrange
            UserRegistrationDto? userRegistrationDto = null;

            // act
            var result = () => _authenticationRepository.IsUserExisted(userRegistrationDto);

            // assert
            result.Should().Throw<ArgumentNullException>();
        }
    }
}
