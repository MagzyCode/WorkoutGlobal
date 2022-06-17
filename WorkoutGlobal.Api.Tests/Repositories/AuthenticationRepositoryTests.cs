using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Threading.Tasks;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.Dto;
using WorkoutGlobal.Api.Repositories;
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
        private readonly Mock<IConfigurationSection> _jwtSettings;

        public AuthenticationRepositoryTests()
        {
            _testConfiguration = ConfigurationAccessor.GetTestConfiguration();
            _jwtSettings = new Mock<IConfigurationSection>();
            _configuration = new Mock<IConfiguration>();
            _configuration
                .Setup(x => x.GetSection("JwtSettings"))
                .Returns(_jwtSettings.Object);

            _mapper = new Mock<IMapper>();
            _mapper
                .Setup(x => x.Map<UserCredentials>(It.IsAny<UserRegistrationDto>()))
                .Returns(new UserCredentials());
            _mapper
                .Setup(x => x.Map<UserCredentials>(It.IsAny<UpdationUserCredentialsDto>()))
                .Returns(new UserCredentials());

            _authenticationRepository = new AuthenticationRepository(
                null,
                null,
                _configuration.Object,
                _mapper.Object);

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
            // arrange
            UpdationUserCredentialsDto? updationUserCredentialsDto = null;

            // act
            var result = async () => await _authenticationRepository.GenerateUserCredentialsAsync(updationUserCredentialsDto);

            // assert
            await result.Should().ThrowAsync<NullReferenceException>();
        }

        [Fact]
        public async Task GenerateUserCredentialsAsync_NullUserPassword_ReturnArgumentNullException()
        {
            // arrange
            var userCredentialsDto = new UpdationUserCredentialsDto()
            {
                UserName = string.Empty,
                Email = string.Empty,
                Password = null
            };

            // act
            var result = await _authenticationRepository.GenerateUserCredentialsAsync(userCredentialsDto);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GenerateUserCredentialsAsync_NullUserPassword_ReturnUserPasswordHash()
        {
            // arrange
            var userCredentialsDto = new UpdationUserCredentialsDto()
            {
                UserName = null,
                Email = null,
                Password = "qwerty123"
            };

            // act
            var result = await _authenticationRepository.GenerateUserCredentialsAsync(userCredentialsDto);

            // assert
            result.Should().BeOfType<UserCredentials>();
            result.PasswordHash.Should().NotBeNull();
        }

        [Fact]
        public async Task GenerateUserCredentialsAsync_ValidUserPassword_ReturnUserPasswordHash()
        {
            // arrange
            var userCredentialsDto = new UpdationUserCredentialsDto()
            {
                UserName = "",
                Email = "",
                Password = "qwerty123"
            };

            // act
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
            result.Should().Throw<NullReferenceException>();
        }

        [Fact]
        public async Task GenerateHashPasswordAsync_ValidUserPasswordCredentials_ReturnHashedPassword()
        {
            // arrange 
            var (password, salt) = ("qwerty123", "123");

            // act
            var result = await _authenticationRepository.GenerateHashPasswordAsync(password, salt);

            // assert

            result.Should().BeOfType(typeof(string));
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().MatchRegex("^([0-9a-z]{1})([0-9a-z]*)$");
        }

        [Fact]
        public async Task GenerateHashPasswordAsync_NullPassword_ReturnHash()
        {
            // arrange 
            var salt = "123";

            // act
            var result = await _authenticationRepository.GenerateHashPasswordAsync(null, salt);

            // assert
            result.Should().Be("a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3");
        }

        [Fact]
        public async Task GenerateHashPasswordAsync_NullPasswordSalt_ReturnHash()
        {
            // arrange 
            var password = "123";

            // act
            var result = await _authenticationRepository.GenerateHashPasswordAsync(password, null);

            // assert
            result.Should().Be("a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3");
        }

        [Fact]
        public async Task GenerateHashPasswordAsync_EmptyPassword_ReturnHash()
        {
            // arrange 
            var (password, salt) = ("", "123");

            // act
            var result = await _authenticationRepository.GenerateHashPasswordAsync(password, salt);

            // assert
            result.Should().Be("a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3");
        }

        [Fact]
        public async Task GenerateHashPasswordAsync_EmptyPasswordSalt_ReturnHash()
        {
            // arrange 
            var (password, salt) = ("qwerty", "");

            // act
            var result = await _authenticationRepository.GenerateHashPasswordAsync(password, salt);

            // assert
            result.Should().Be("65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5");
        }
    }
}
