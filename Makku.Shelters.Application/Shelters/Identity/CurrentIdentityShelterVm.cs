using Makku.Shelters.Application.Common.Mappings;
using Makku.Shelters.Domain.ShelterProfileAggregate;

namespace Makku.Shelters.Application.Shelters.Identity
{
    public class CurrentIdentityShelterVm : IMapWith<ShelterProfile>
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string ShelterName { get; set; }
        public string Token { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<ShelterProfile, CurrentIdentityShelterVm>()
                .ForMember(dest => dest.EmailAddress, opt
                    => opt.MapFrom(src => src.BasicInfo.Email))
                .ForMember(dest => dest.ShelterName, opt
                    => opt.MapFrom(src => src.BasicInfo.ShelterName));
        }
    }
}
