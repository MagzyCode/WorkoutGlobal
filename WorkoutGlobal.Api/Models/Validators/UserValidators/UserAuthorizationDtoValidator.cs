using FluentValidation;
using WorkoutGlobal.Api.Models.DTOs.UserDTOs;

namespace WorkoutGlobal.Api.Models.Validators.UserValidators
{
    /// <summary>
    /// Fluent Validator for UserAuthorizationDto.
    /// </summary>
    public class UserAuthorizationDtoValidator : AbstractValidator<UserAuthorizationDto>
    {
        /// <summary>
        /// Sets rules for authorization model.
        /// </summary>
        public UserAuthorizationDtoValidator()
        {
            RuleFor(user => user.UserName)
                .NotNull()
                .NotEmpty();

            RuleFor(user => user.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
