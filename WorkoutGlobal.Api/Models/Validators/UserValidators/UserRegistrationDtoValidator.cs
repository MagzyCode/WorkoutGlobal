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
            RuleFor(user => user.Name)
                .NotNull()
                .NotEmpty()
                .Length(2, 50);

            RuleFor(user => user.Surname)
                .NotNull()
                .NotEmpty()
                .Length(2, 50);

            RuleFor(user => user.Patronymic)
                .NotNull()
                .NotEmpty()
                .Length(2, 50);

            RuleFor(user => user.DateOfBirth)
                .NotNull()
                .NotEmpty()
                .GreaterThan(DateTime.Parse("01/01/1900 00:00:00"));

            RuleFor(user => user.Country)
                .NotNull()
                .NotEmpty()
                .Length(2, 150);

            RuleFor(user => user.City)
                .NotNull()
                .NotEmpty()
                .Length(2, 150);

            RuleFor(user => user.Street)
                .NotNull()
                .NotEmpty()
                .Length(2, 150);

            RuleFor(user => user.StreetNumber)
                .NotNull()
                .NotEmpty()
                .Length(1, 6);

            When(user => user.StreetNumber != null, () =>
            {
                RuleFor(user => user.RoomNumber)
                    .Length(1, 8);
            });
        }
    }
}
