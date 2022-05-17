using WorkoutGlobal.Api.Controllers;
using Moq;
using Xunit;
using AutoMapper;
using WorkoutGlobal.Api.Contracts.RepositoryManagerContracts;
using System.Threading.Tasks;
using WorkoutGlobal.Api.Models.DTOs.UserDTOs;
using WorkoutGlobal.Api.Contracts.AuthenticationManagerContracts;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using WorkoutGlobal.Api.Models.ErrorModels;
using Microsoft.AspNetCore.Http;

namespace WorkoutGlobal.Api.Tests.Controllers
{
    public class AuthenticationControllerTests
    {
        private readonly AuthenticationController _authenticationController;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;

        public AuthenticationControllerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _authenticationController = new AuthenticationController(_mapperMock.Object, _repositoryManagerMock.Object);
        }

        [Fact]
        public async Task Authenticate_ExistedAuthorizationUser_ReturnOkObjectResultWithToken()
        {
            // arrange
            var authenticationRepositoryMock = new Mock<IAuthenticationRepository>();
            _repositoryManagerMock.Setup(x => x.AuthenticationRepository).Returns(authenticationRepositoryMock.Object);
            authenticationRepositoryMock.Setup(x => x.ValidateUserAsync(It.IsAny<UserAuthorizationDto>())).Returns(Task.FromResult(true));
            authenticationRepositoryMock.Setup(x => x.CreateToken(It.IsAny<UserAuthorizationDto>())).Returns("aaa.aaa.aaa");

            // act
            var result = await _authenticationController.Authenticate(new UserAuthorizationDto());

            // assert
            result.Should().NotBeNull()
                .And.BeOfType<OkObjectResult>()
                .Which.Value.Should().NotBeNull()
                    .And.BeOfType<string>()
                    .Which.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Authenticate_NotExistedAuthorizationUser_ReturnUnauthorizedObjectResult()
        {
            // arrange
            var authenticationRepositoryMock = new Mock<IAuthenticationRepository>();
            _repositoryManagerMock.Setup(x => x.AuthenticationRepository).Returns(authenticationRepositoryMock.Object);

            // act
            var result = await _authenticationController.Authenticate(new UserAuthorizationDto());

            // assert
            result.Should().NotBeNull()
                .And.BeOfType<UnauthorizedObjectResult>()
                .Which.Value.Should().NotBeNull()
                    .And.BeOfType<ErrorDetails>()
                    .Which.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public async Task Registrate_ValidRegistrationUser_Return201StatusCode()
        {
            // arrange
            var userRegistrationDto = new UserRegistrationDto();
            var authenticationRepositoryMock = new Mock<IAuthenticationRepository>();
            _repositoryManagerMock.Setup(x => x.AuthenticationRepository).Returns(authenticationRepositoryMock.Object);

            // act
            var result = await _authenticationController.Registrate(userRegistrationDto);

            // assert
            result.Should().NotBeNull()
                .And.BeOfType<StatusCodeResult>()
                .Which.StatusCode.Should().Be(StatusCodes.Status201Created);
        }

        [Fact]
        public async Task Registrate_UserExisted_Return400StatusCode()
        {
            // arrange
            var userRegistrationDto = new UserRegistrationDto();
            var authenticationRepositoryMock = new Mock<IAuthenticationRepository>();
            _repositoryManagerMock.Setup(x => x.AuthenticationRepository).Returns(authenticationRepositoryMock.Object);
            authenticationRepositoryMock.Setup(x => x.IsUserExisted(It.IsAny<UserRegistrationDto>())).Returns(true);

            // act
            var result = await _authenticationController.Registrate(userRegistrationDto);

            // assert
            result.Should().NotBeNull()
                .And.BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().HaveValue()
                    .And.Be(StatusCodes.Status400BadRequest);
        }
    }
}
