using AutoMapper;
using WorkoutGlobal.Api.Models.DTOs.CategoryDTOs;
using WorkoutGlobal.Api.Models.DTOs.CommentDTOs;
using WorkoutGlobal.Api.Models.DTOs.CommentsBlockDTOs;
using WorkoutGlobal.Api.Models.DTOs.CourseDTOs;
using WorkoutGlobal.Api.Models.DTOs.OrderDTOs;
using WorkoutGlobal.Api.Models.DTOs.PostDTOs;
using WorkoutGlobal.Api.Models.DTOs.SportEventDTOs;
using WorkoutGlobal.Api.Models.DTOs.StoreVideoDTOs;
using WorkoutGlobal.Api.Models.DTOs.SubscribeCourseDTOs;
using WorkoutGlobal.Api.Models.DTOs.SubscribeEventDTOs;
using WorkoutGlobal.Api.Models.DTOs.UserDTOs;
using WorkoutGlobal.Api.Models.DTOs.VideoDTOs;

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
            CreateMap<UserRegistrationDto, UserCredentialsDto>();
            CreateMap<UserAuthorizationDto, UserCredentialsDto>();
            CreateMap<UserCredentialsDto, UserCredentials>();
            CreateMap<DTOs.UserCredentialDTOs.UserCredentialDto, UserCredentials>().ReverseMap();

            CreateMap<UserRegistrationDto, User>();
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<Video, VideoDto>().ReverseMap();
            CreateMap<Video, CreationVideoDto>().ReverseMap();

            CreateMap<Comment, CommentDto>().ReverseMap();

            CreateMap<CommentsBlockDto, CommentsBlock>().ReverseMap();

            CreateMap<Course, CourseDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<SportEvent, SportEventDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();

            CreateMap<Post, PostDto>().ReverseMap();

            CreateMap<StoreVideo, StoreVideoDto>().ReverseMap();

            CreateMap<SubscribeCourse, SubscribeCourseDto>().ReverseMap();

            CreateMap<SubscribeEvent, SubscribeEventDto>().ReverseMap();
        }
    }
}
