using AutoMapper;
using WorkoutGlobal.UI.ViewModels;
using WorkoutGlobal.UI.ViewModels.Authentication;

namespace WorkoutGlobal.UI.Models.Mapping
{
    /// <summary>
    /// Represents auto mapping rules.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Sets mapping rules.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<UserAuthorizationViewModel, AuthenticationUser>();
            CreateMap<UserRegistrationViewModel, UserCredentials>();
            CreateMap<UserRegistrationViewModel, AuthenticationUser>();
            CreateMap<RegistrationUser, AuthenticationUser>();

            CreateMap<RegistrationUser, UserRegistrationViewModel>().ReverseMap();

            CreateMap<Video, VideoWithCommentsViewModel>().ReverseMap();
            CreateMap<Video, CreationVideoViewModel>().ReverseMap();

            CreateMap<Comment, CommentViewModel>().ReverseMap();

            CreateMap<CommentsBlock, CommentsBlockViewModel>().ReverseMap();

            CreateMap<Course, CourseViewModel>().ReverseMap();
        }
    }
}
