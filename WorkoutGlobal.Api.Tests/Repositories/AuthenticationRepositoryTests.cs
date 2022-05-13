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

        public AuthenticationRepositoryTests()
        {
            _testConfiguration = ConfigurationAccessor.GetTestConfiguration();
            _configuration = new Mock<IConfiguration>();
            _mapper = new Mock<IMapper>();
            _userCredentialsRepository = new Mock<IUserCredentialsRepository>();
            _authenticationRepository = new AuthenticationRepository(
                null, 
                null,
                _configuration.Object, 
                _userCredentialsRepository.Object, 
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

            var jwtSetting = new Mock<IConfigurationSection>();
            _configuration
                .Setup(x => x.GetSection("JwtSettings"))
                .Returns(jwtSetting.Object);
            jwtSetting
                .Setup(x => x.GetSection("Key").Value)
                .Returns(_testConfiguration["JwtSettingsValid:Key"]);
            jwtSetting
                .Setup(x => x.GetSection("ValidIssuer").Value)
                .Returns(_testConfiguration["JwtSettingsValid:ValidIssuer"]);
            jwtSetting
                .Setup(x => x.GetSection("ValidAudience").Value)
                .Returns(_testConfiguration["JwtSettingsValid:ValidAudience"]);    
            jwtSetting
                .Setup(x => x.GetSection("Expires").Value)
                .Returns(_testConfiguration["JwtSettingsValid:Expires"]);

            // act
            var result = _authenticationRepository.CreateToken(userAuthorizationDto);

            // assert
            result.Should().BeOfType<string>()
                .Which.Should().NotBeNullOrWhiteSpace()
                .And.MatchEquivalentOf("*.*.*");
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

            var jwtSetting = new Mock<IConfigurationSection>();
            _configuration
                .Setup(x => x.GetSection("JwtSettings"))
                .Returns(jwtSetting.Object);
            jwtSetting
                .Setup(x => x.GetSection("Key").Value)
                .Returns(_testConfiguration["JwtSettingsInvalidKeyLength:Key"]);
            jwtSetting
                .Setup(x => x.GetSection("ValidIssuer").Value)
                .Returns(_testConfiguration["JwtSettingsInvalidKeyLength:ValidIssuer"]);
            jwtSetting
                .Setup(x => x.GetSection("ValidAudience").Value)
                .Returns(_testConfiguration["JwtSettingsInvalidKeyLength:ValidAudience"]);
            jwtSetting
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

            var jwtSetting = new Mock<IConfigurationSection>();
            _configuration
                .Setup(x => x.GetSection("JwtSettings"))
                .Returns(jwtSetting.Object);
            jwtSetting
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

            var jwtSetting = new Mock<IConfigurationSection>();
            _configuration
                .Setup(x => x.GetSection("JwtSettings"))
                .Returns(jwtSetting.Object);
            jwtSetting
                .Setup(x => x.GetSection("Key").Value)
                .Returns(_testConfiguration["JwtSettingsInvalidExpires:Key"]);
            jwtSetting
                .Setup(x => x.GetSection("ValidIssuer").Value)
                .Returns(_testConfiguration["JwtSettingsInvalidExpires:ValidIssuer"]);
            jwtSetting
                .Setup(x => x.GetSection("ValidAudience").Value)
                .Returns(_testConfiguration["JwtSettingsInvalidExpires:ValidAudience"]);
            jwtSetting
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

            var jwtSetting = new Mock<IConfigurationSection>();
            _configuration
                .Setup(x => x.GetSection("JwtSettings"))
                .Returns(jwtSetting.Object);
            jwtSetting
                .Setup(x => x.GetSection("Key").Value)
                .Returns(_testConfiguration["JwtSettingsValid:Key"]);
            jwtSetting
                .Setup(x => x.GetSection("ValidIssuer").Value)
                .Returns(_testConfiguration["JwtSettingsValid:ValidIssuer"]);
            jwtSetting
                .Setup(x => x.GetSection("ValidAudience").Value)
                .Returns(_testConfiguration["JwtSettingsValid:ValidAudience"]);
            jwtSetting
                .Setup(x => x.GetSection("Expires").Value)
                .Returns(_testConfiguration["JwtSettingsValid:Expires"]);

            // act
            var result = _authenticationRepository.CreateToken(userAuthorizationDto);

            // assert
            result.Should().BeOfType<string>()
                .Which.Should().NotBeNullOrWhiteSpace()
                .And.MatchEquivalentOf("*.*.*");
        }

        [Fact]
        public async Task GenerateUserCredentialsAsync_NullUserCredentials_ReturnNullReferenceException()
        {
            // act
            UserCredentialsDto? userCredentialsDto = null;
            _mapper
                .Setup(x => x.Map<UserCredentials>(userCredentialsDto))
                .Returns(new UserCredentials());

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
            _mapper
                .Setup(x => x.Map<UserCredentials>(userCredentialsDto))
                .Returns(new UserCredentials());
            _userCredentialsRepository
                .Setup(x => x.GetHashPasswordAsync(null, It.IsAny<string>()))
                .Throws(() => new ArgumentNullException());

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
            _mapper
                .Setup(x => x.Map<UserCredentials>(userCredentialsDto))
                .Returns(new UserCredentials());
            _userCredentialsRepository
                .Setup(x => x.GetHashPasswordAsync("qwerty123", It.IsAny<string>()))
                .Returns(Task.FromResult(string.Empty));

            // arrange
            var result = await _authenticationRepository.GenerateUserCredentialsAsync(userCredentialsDto);

            // assert
            result.Should().BeOfType<UserCredentials>()
                .Which.PasswordHash.Should().NotBeNull();
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
            _mapper
                .Setup(x => x.Map<UserCredentials>(userCredentialsDto))
                .Returns(new UserCredentials());
            _userCredentialsRepository
                .Setup(x => x.GetHashPasswordAsync("qwerty123", It.IsAny<string>()))
                .Returns(Task.FromResult(string.Empty));

            // arrange
            var result = await _authenticationRepository.GenerateUserCredentialsAsync(userCredentialsDto);

            // assert
            result.Should().BeOfType<UserCredentials>()
                .Which.PasswordHash.Should().NotBeNull();
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
