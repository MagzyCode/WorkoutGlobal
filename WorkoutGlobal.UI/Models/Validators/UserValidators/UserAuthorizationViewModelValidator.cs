using FluentValidation;
using WorkoutGlobal.UI.ViewModels;

namespace WorkoutGlobal.UI.Models.Validators.UserValidators
{
    /// <summary>
    /// Fluent Validator for UserAuthorizationViewModel.
    /// </summary>
    public class UserAuthorizationViewModelValidator : AbstractValidator<UserAuthorizationViewModel>
    {
        /// <summary>
        /// Sets rules for authorization model.
        /// </summary>
        public UserAuthorizationViewModelValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty();

            RuleFor(user => user.Password)
                .NotEmpty();
        }
    }
}
