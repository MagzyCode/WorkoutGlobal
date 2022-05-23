using AutoMapper;
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
            CreateMap<UserRegistrationDto, User>();

            CreateMap<Video, VideoDto>().ReverseMap();
            CreateMap<Video, CreationVideoDto>().ReverseMap();
        }
    }
}
