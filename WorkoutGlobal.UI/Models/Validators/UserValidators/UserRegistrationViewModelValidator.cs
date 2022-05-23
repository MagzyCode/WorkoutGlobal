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

            RuleFor(user => user.FirstName)
                .NotEmpty()
                .Length(2, 100)
                .Matches(@"^([A-Za-zА-Яа-я])([A-Za-zА-Яа-я]){2,100}$")
                    .WithMessage("Name should contain only cyrillic or english letters");

            RuleFor(user => user.LastName)
                .NotEmpty()
                .Length(2, 100)
                .Matches(@"^([A-Za-zА-Яа-я])([A-Za-zА-Яа-я]){2,100}$")
                    .WithMessage("Name should contain only cyrillic or english letters");

            RuleFor(user => user.Patronymic)
                .NotEmpty()
                .Length(2, 100)
                .Matches(@"^([A-Za-zА-Яа-я])([A-Za-zА-Яа-я]){2,100}$")
                    .WithMessage("Name should contain only cyrillic or english letters");

            RuleFor(user => user.DateOfBirth)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Parse("01/01/1899"));

            RuleFor(user => user.ResidencePlace)
                .NotEmpty()
                .Length(5, 200);

            When(user => user.Height != null, () => {
                RuleFor(user => user.Height)
                    .NotEmpty()
                    .InclusiveBetween(50, 300);
            });

            When(user => user.Weight != null, () => {
                RuleFor(user => user.Weight)
                    .NotEmpty()
                    .InclusiveBetween(10, 300);
            });

            RuleFor(user => user.ClassificationNumber)
                .NotEmpty()
                .Length(10, 25)
                .Matches(@"^([A-Za-z0-9])([[A-Za-z0-9]){10,25}$");
        }
    }
}
