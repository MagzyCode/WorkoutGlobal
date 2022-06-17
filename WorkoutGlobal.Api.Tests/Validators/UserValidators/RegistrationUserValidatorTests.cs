using FluentAssertions;
using FluentValidation.Results;
using System.Threading.Tasks;
using WorkoutGlobal.Api.Models.Dto;
using WorkoutGlobal.Api.Models.Validators;
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
            };

            // act
            var validationResult = await validator.ValidateAsync(userRegistrationUserDto);

            // assert
            validationResult.Should().BeOfType(typeof(ValidationResult));
            validationResult.Should().NotBeNull();
            validationResult.Errors.Should().HaveCount(3);
            validationResult.IsValid.Should().BeFalse();
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
            };

            // act
            var validationResult = await validator.ValidateAsync(userRegistrationUserDto);

            // assert
            validationResult.Should().BeOfType(typeof(ValidationResult));
            validationResult.Should().NotBeNull();
            validationResult.Errors.Should().HaveCount(3);
            validationResult.IsValid.Should().BeFalse();
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
            };

            // act
            var validationResult = await validator.ValidateAsync(userRegistrationUserDto);

            // assert
            validationResult.Should().BeOfType(typeof(ValidationResult));
            validationResult.Should().NotBeNull();
            validationResult.Errors.Should().HaveCount(2);
            validationResult.IsValid.Should().BeFalse();
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
            };

            // act
            var validationResult = await validator.ValidateAsync(userRegistrationUserDto);

            // assert
            validationResult.Should().BeOfType(typeof(ValidationResult));
            validationResult.Should().NotBeNull();
            validationResult.Errors.Should().HaveCount(2);
            validationResult.IsValid.Should().BeFalse();
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
            };

            // act
            var validationResult = await validator.ValidateAsync(userRegistrationUserDto);

            // assert
            validationResult.Should().BeOfType(typeof(ValidationResult));
            validationResult.Should().NotBeNull();
            validationResult.Errors.Should().HaveCount(1);
            validationResult.IsValid.Should().BeFalse();
            validationResult.ToString().Should().BeEquivalentTo("'Email' is not a valid email address.");
        }
    }
}
