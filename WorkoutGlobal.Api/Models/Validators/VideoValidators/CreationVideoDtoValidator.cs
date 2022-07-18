using FluentValidation;
using WorkoutGlobal.Api.Models.Dto;

namespace WorkoutGlobal.Api.Models.Validators.VideoValidators
{
    public class CreationVideoDtoValidator : AbstractValidator<CreationVideoDto>
    {
        public CreationVideoDtoValidator()
        {
            RuleFor(video => video.Link)
                .NotEmpty();

            RuleFor(video => video.Title)
               .NotEmpty();

            RuleFor(video => video.Description)
               .NotEmpty();

            RuleFor(video => video.CategoryId)
               .NotEmpty();

            RuleFor(video => video.UserId)
               .NotEmpty();
        }

    }
}
