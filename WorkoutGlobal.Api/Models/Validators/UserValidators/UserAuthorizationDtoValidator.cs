using FluentValidation;
using WorkoutGlobal.Api.Models.Dto;

namespace WorkoutGlobal.Api.Models.Validators
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
                .NotEmpty();

            RuleFor(user => user.Password)
                .NotEmpty();
        }
    }
}
