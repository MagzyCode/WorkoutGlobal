using WorkoutGlobal.Api.Controllers;
using Moq;
using Xunit;
using AutoMapper;
using WorkoutGlobal.Api.Contracts;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using WorkoutGlobal.Api.Models.ErrorModels;
using Microsoft.AspNetCore.Http;
using WorkoutGlobal.Api.Models.Dto;

namespace WorkoutGlobal.Api.Tests.Controllers
{
    public class AuthenticationControllerTests
    {
        private readonly AuthenticationController _authenticationController;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly Mock<IAuthenticationRepository> _authenticationRepositoryMock;

        public AuthenticationControllerTests()
        {
            _mapperMock = new Mock<IMapper>();

            _authenticationRepositoryMock = new Mock<IAuthenticationRepository>();
            _authenticationRepositoryMock.Setup(x => x.ValidateUserAsync(It.IsAny<UserAuthorizationDto>())).Returns(Task.FromResult(true));
            _authenticationRepositoryMock.Setup(x => x.CreateToken(It.IsAny<UserAuthorizationDto>())).Returns("aaa.aaa.aaa");
            _authenticationRepositoryMock.Setup(x => x.IsUserExisted(It.IsAny<UserRegistrationDto>())).Returns(true);

            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.AuthenticationRepository).Returns(_authenticationRepositoryMock.Object);

            _authenticationController = new AuthenticationController(_mapperMock.Object, _repositoryManagerMock.Object);
        }

        [Fact]
        public async Task Authenticate_ExistedAuthorizationUser_ReturnOkObjectResultWithToken()
        {
            // arrange
            var userAuthorizationDto = new UserAuthorizationDto();

            // act
            var result = await _authenticationController.Authenticate(userAuthorizationDto);

            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result.As<OkObjectResult>();
            okResult.Value.Should().NotBeNull();
            okResult.Value.Should().BeOfType<string>();
            okResult.Value.As<string>().Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Authenticate_NotExistedAuthorizationUser_ReturnUnauthorizedObjectResult()
        {
            // arrange
            var userAuthorizationDto = new UserAuthorizationDto();
            _authenticationRepositoryMock.Setup(x => x.ValidateUserAsync(It.IsAny<UserAuthorizationDto>())).Returns(Task.FromResult(false));

            // act
            var result = await _authenticationController.Authenticate(userAuthorizationDto);

            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(UnauthorizedObjectResult));

            var unauthorizedResult = result.As<UnauthorizedObjectResult>();
            unauthorizedResult.Value.Should().NotBeNull();
            unauthorizedResult.Value.Should().BeOfType(typeof(ErrorDetails));
            unauthorizedResult.Value.As<ErrorDetails>().StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public async Task Registrate_ValidRegistrationUser_Return201StatusCode()
        {
            // arrange
            var userRegistrationDto = new UserRegistrationDto();
            _authenticationRepositoryMock.Setup(x => x.IsUserExisted(It.IsAny<UserRegistrationDto>())).Returns(false);

            // act
            var result = await _authenticationController.Registrate(userRegistrationDto);

            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(StatusCodeResult));
            result.As<StatusCodeResult>().StatusCode.Should().Be(StatusCodes.Status201Created);
        }

        [Fact]
        public async Task Registrate_UserExisted_Return400StatusCode()
        {
            // arrange
            var userRegistrationDto = new UserRegistrationDto();

            // act
            var result = await _authenticationController.Registrate(userRegistrationDto);

            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(UnauthorizedObjectResult));

            var badRequestResult = result.As<UnauthorizedObjectResult>();
            badRequestResult.StatusCode.Should().HaveValue();
            badRequestResult.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }
    }
}
