using AutoMapper;
using Makku.Shelters.Application.Identity.Dtos;
using Makku.Shelters.Domain.ShelterProfileAggregate;

namespace Makku.Shelters.Application.Identity.MappingProfiles;

public class IdentityProfiles : Profile
{
    public IdentityProfiles()
    {
        CreateMap<ShelterProfile, IdentityShelterProfileDto>()
            .ForMember(dest => dest.EmailAddress, opt
                => opt.MapFrom(src => src.BasicInfo.Email))
            .ForMember(dest => dest.ShelterName, opt
                => opt.MapFrom(src => src.BasicInfo.ShelterName));

    }
}