using FluentValidation;
using WorkoutGlobal.UI.ViewModels.Authentication;

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
                .NotNull()
                .NotEmpty();

            RuleFor(user => user.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
