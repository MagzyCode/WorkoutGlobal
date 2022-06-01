using FluentValidation;
using WorkoutGlobal.Api.Models.Dto;

namespace WorkoutGlobal.Api.Models.Validators
{
    /// <summary>
    /// Fluent Validator for UserRegistrationDto.
    /// </summary>
    public class UserRegistrationDtoValidator : AbstractValidator<UserRegistrationDto>
    {
        /// <summary>
        /// Sets validation rules for registration user.
        /// </summary>
        public UserRegistrationDtoValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(user => user.UserName)
                .NotEmpty()
                .Length(3, 40)
                .Matches(@"^([A-Za-z0-9_=+])([A-Za-z0-9_=+]){2,50}$")
                    .WithMessage("Check your '{PropertyName}' for using forbidden сharacters (@%?#<>%/) and cyrillic.");

            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress();
            
            RuleFor(user => user.Password)
                .NotEmpty()
                .Length(6, 50)
                .Matches(@"^([A-Za-z0-9_=+])([A-Za-z0-9_=+]){5,50}$")
                    .WithMessage("Check your '{PropertyName}' for using forbidden сharacters (@%?#<>%/) and cyrillic.");

            RuleFor(user => user.ConfirmPassword)
                .NotEmpty()
                .Equal(user => user.Password);
        }
    }
}
