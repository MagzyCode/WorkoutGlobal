using System.Threading.Tasks;
using WorkoutGlobal.Api.Models.Validators;
using FluentAssertions;
using Xunit;
using WorkoutGlobal.Api.Models.Dto;
using FluentValidation.Results;

namespace WorkoutGlobal.Api.Tests.Validators.UserValidators
{
    public class AuthorizationUserValidatorTests
    {
        private readonly UserAuthorizationDtoValidator validator = new();

        [Fact]
        public async Task ModelState_NullUserCredentials_ReturnValidationResult()
        {
            // arrange
            var userAuthorizationDto = new UserAuthorizationDto()
            {
                UserName = null,
                Password = null
            };

            // act
            var validationResult = await validator.ValidateAsync(userAuthorizationDto);

            // assert
            validationResult.Should().BeOfType(typeof(ValidationResult));
            validationResult.Should().NotBeNull();
            validationResult.Errors.Should().HaveCount(2);
            validationResult.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ModelState_EmptyUserCredentials_ReturnValidationResult()
        {
            // arrange
            var userAuthorizationDto = new UserAuthorizationDto()
            {
                UserName = "",
                Password = ""
            };

            // act
            var result = await validator.ValidateAsync(userAuthorizationDto);

            // assert
            result.Should().BeOfType(typeof(ValidationResult));
            result.Should().NotBeNull();
            result.Errors.Should().HaveCount(2);
            result.IsValid.Should().BeFalse();
        }
    }
}
