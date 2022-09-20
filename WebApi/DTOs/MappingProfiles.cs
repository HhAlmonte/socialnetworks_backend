using AutoMapper;
using Core.Entities;

namespace WebApi.DTOs
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // user
            CreateMap<UserEntities, ResponseUserDto>();
            CreateMap<RegistrationDto, UserEntities>();
            
            // publications
            CreateMap<PublicationDto, PublicationsEntities>();
            CreateMap<PublicationsEntities, ResponsePublicationDto>();
        }
    }
}
