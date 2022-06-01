using AutoMapper;
using WorkoutGlobal.Api.Models.Dto;

namespace WorkoutGlobal.Api.Models.Mapping
{
    /// <summary>
    /// Class for configure mapping rules via AutoMapper library.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Sets mapping rules for models and their DTOs.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<UserRegistrationDto, UserCredentials>();
            CreateMap<UserRegistrationDto, UserWithCredentialsDto>();
            CreateMap<UserAuthorizationDto, UserWithCredentialsDto>();
            CreateMap<UserWithCredentialsDto, UserCredentials>();
            CreateMap<UserCredentialDto, UserCredentials>().ReverseMap();

            CreateMap<UserRegistrationDto, User>();
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<Video, VideoDto>().ReverseMap();
            CreateMap<Video, CreationVideoDto>().ReverseMap();

            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Comment, CreationCommentDto>().ReverseMap();

            CreateMap<CommentsBlockDto, CommentsBlock>().ReverseMap();
            CreateMap<CreationCommentsBlockDto, CommentsBlock>().ReverseMap();

            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CreationCourseDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreationCategoryDto>().ReverseMap();

            CreateMap<SportEvent, SportEventDto>().ReverseMap();
            CreateMap<SportEvent, CreationSportEventDto>().ReverseMap();

            CreateMap<CourseVideo, CreationCourseVideoDto>().ReverseMap();
            CreateMap<CourseVideo, CourseVideoDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();

            CreateMap<Post, PostDto>().ReverseMap();

            CreateMap<StoreVideo, StoreVideoDto>().ReverseMap();
            CreateMap<StoreVideo, CreationStoreVideoDto>().ReverseMap();

            CreateMap<SubscribeCourse, SubscribeCourseDto>().ReverseMap();
            CreateMap<SubscribeCourse, CreationSubscribeCourseDto>().ReverseMap();

            CreateMap<SubscribeEvent, SubscribeEventDto>().ReverseMap();
            CreateMap<SubscribeEvent, CreationSubscribeEventDto>().ReverseMap();
        }
    }
}
