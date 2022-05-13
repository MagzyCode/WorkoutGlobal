using FluentAssertions;
using FluentValidation.Results;
using System.Threading.Tasks;
using WorkoutGlobal.Api.Models.DTOs.UserDTOs;
using WorkoutGlobal.Api.Models.Validators.UserValidators;
using Xunit;

namespace WorkoutGlobal.Api.Tests.Validators.UserValidators
{
    public class RegistrationUserValidatorTests
    {
        private readonly UserRegistrationDtoValidator validator = new();

        [Fact]
        public async Task ModelState_NullRegistrationCredentials_ReturnValidationResult()
        {
            // arrange
            var userRegistrationUserDto = new UserRegistrationDto()
            {
                UserName = null,
                Email = null,
                Password = null,
                ConfirmPassword = null
            };

            // act
            var validationResult = await validator.ValidateAsync(userRegistrationUserDto);

            // assert
            validationResult
                .Should().BeOfType<ValidationResult>()
                .Which.Errors.Should().HaveCount(4);
            validationResult.IsValid
                .Should().BeFalse();
        }

        [Fact]
        public async Task ModelState_EmptyRegistrationCredentials_ReturnValidationResult()
        {
            // arrange
            var userRegistrationUserDto = new UserRegistrationDto()
            {
                UserName = "",
                Email = "",
                Password = "",
                ConfirmPassword = ""
            };

            // act
            var validationResult = await validator.ValidateAsync(userRegistrationUserDto);

            // assert
            validationResult
                .Should().BeOfType<ValidationResult>()
                .Which.Errors.Should().HaveCount(4);
            validationResult.IsValid
                .Should().BeFalse();
        }

        [Fact]
        public async Task ModelState_IncorrectUserNameAndPasswordLength_ReturnValidationResult()
        {
            // arrange
            var userRegistrationUserDto = new UserRegistrationDto()
            {
                UserName = "AA",
                Email = "aa@mail.com",
                Password = "asdas",
                ConfirmPassword = "asdas"
            };

            // act
            var validationResult = await validator.ValidateAsync(userRegistrationUserDto);

            // assert
            validationResult
                .Should().BeOfType<ValidationResult>()
                .Which.Errors.Should().HaveCount(2);
            validationResult.IsValid
                .Should().BeFalse();
        }

        [Fact]
        public async Task ModelState_IncorrectUserNameAndPasswordPattern_ReturnValidationResult()
        {
            // arrange
            var userRegistrationUserDto = new UserRegistrationDto()
            {
                UserName = "alpha bet",
                Email = "aa@mail.com",
                Password = "zqwert 123",
                ConfirmPassword = "zqwert 123"
            };

            // act
            var validationResult = await validator.ValidateAsync(userRegistrationUserDto);

            // assert
            validationResult
                .Should().BeOfType<ValidationResult>()
                .Which.Errors.Should().HaveCount(2);
            validationResult.IsValid
                .Should().BeFalse();
        }

        [Fact]
        public async Task ModelState_IncorrectUserEmail_ReturnValidationResult()
        {
            // arrange
            var userRegistrationUserDto = new UserRegistrationDto()
            {
                UserName = "alphabet",
                Email = "@mail.com",
                Password = "zqwert123",
                ConfirmPassword = "zqwert123"
            };

            // act
            var validationResult = await validator.ValidateAsync(userRegistrationUserDto);

            // assert
            validationResult
                .Should().BeOfType<ValidationResult>()
                .Which.Errors.Should().HaveCount(1);
            validationResult.IsValid
                .Should().BeFalse();
            validationResult.ToString().Should().BeEquivalentTo("'Email' is not a valid email address.");
        }

        [Fact]
        public async Task ModelState_IncorrectConfirmPassword_ReturnValidationResult()
        {
            // arrange
            var userRegistrationUserDto = new UserRegistrationDto()
            {
                UserName = "alphabet",
                Email = "a@mail.com",
                Password = "zqwert123",
                ConfirmPassword = "zqwert1234"
            };

            // act
            var validationResult = await validator.ValidateAsync(userRegistrationUserDto);

            // assert
            validationResult
                .Should().BeOfType<ValidationResult>()
                .Which.Errors.Should().HaveCount(1);
            validationResult.IsValid
                .Should().BeFalse();
            validationResult.ToString().Should().BeEquivalentTo("'Confirm Password' must be equal to 'zqwert123'.");
        }
    }
}
