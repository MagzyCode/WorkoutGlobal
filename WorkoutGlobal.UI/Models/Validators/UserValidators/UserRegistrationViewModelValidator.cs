using FluentValidation;
using WorkoutGlobal.UI.ViewModels.Authentication;

namespace WorkoutGlobal.UI.Models.Validators.UserValidators
{
    /// <summary>
    /// Validator for RegistrationViewModel class.
    /// </summary>
    public class UserRegistrationViewModelValidator : AbstractValidator<UserRegistrationViewModel>
    {
        /// <summary>
        /// Ctor for registration view model validator.
        /// </summary>
        public UserRegistrationViewModelValidator()
        {
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
