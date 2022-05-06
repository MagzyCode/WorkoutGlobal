using FluentValidation;
using WorkoutGlobal.Api.Models.DTOs.UserDTOs;

namespace WorkoutGlobal.Api.Models.Validators.UserValidators
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
            RuleFor(user => user.UserName)
                .NotNull()
                .NotEmpty()
                .Length(3, 40)
                .Matches(@"[^\sА-Яа-я@%?#<>%/]")
                    .WithMessage("Check your '{PropertyName}' for using forbidden сharacters (@%?#<>%/) and cyrillic.");

            RuleFor(user => user.Email)
                .NotNull()
                .EmailAddress();
            
            RuleFor(user => user.Password)
                .NotNull()
                .NotEmpty()
                .Length(6, 50)
                .Matches(@"[^\sА-Яа-я@%?#<>%/]")
                    .WithMessage("Check your '{PropertyName}' for using forbidden сharacters (@%?#<>%/) and cyrillic.");

            RuleFor(user => user.ConfirmPassword)
                .NotNull()
                .NotEmpty()
                .Equal(user => user.Password);
        }
    }
}
