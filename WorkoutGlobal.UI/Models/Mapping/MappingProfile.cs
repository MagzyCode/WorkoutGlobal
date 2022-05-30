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

            CreateMap<Video, VideoWithCommentsAndSubscriptionViewModel>().ReverseMap();
            CreateMap<Video, CreationVideoViewModel>().ReverseMap();
            CreateMap<Video, VideoViewModel>().ReverseMap();


            CreateMap<Comment, CommentViewModel>().ReverseMap();

            CreateMap<CommentsBlock, CommentsBlockViewModel>().ReverseMap();

            CreateMap<Course, CourseViewModel>().ReverseMap();
            CreateMap<Course, CreationCourseViewModel>().ReverseMap();

            CreateMap<Category, CategoryViewModel>().ReverseMap();
            
            CreateMap<User, UserViewModel>().ReverseMap();

            CreateMap<SportEvent, SportEventViewModel>().ReverseMap();

            CreateMap<StoreVideo, StoreVideoViewModel>().ReverseMap();

            CreateMap<SubscribeCourse, SubscribeCourseViewModel>().ReverseMap();

            CreateMap<SubscribeEvent, SubscribeEventViewModel>().ReverseMap();
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<Post, PostViewModel>().ReverseMap();

            CreateMap<UserCredentialsModel, UserCredentialsModelViewModel>().ReverseMap();
        }
    }
}
