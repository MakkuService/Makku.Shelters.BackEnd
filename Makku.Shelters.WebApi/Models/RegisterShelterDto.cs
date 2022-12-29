using AutoMapper;
using Makku.Shelters.Application.Common.Mappings;
using Makku.Shelters.Application.Shelters.Identity.Commands.RegisterShelter;

namespace Makku.Shelters.WebApi.Models
{
    public class RegisterShelterDto : IMapWith<RegisterShelterCommand>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ShelterName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisterShelterDto, RegisterShelterCommand>()
                .ForMember(dest => dest.Email, opt
                    => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.ShelterName, opt
                    => opt.MapFrom(src => src.ShelterName))
                .ForMember(dest => dest.Password, opt
                => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Username, opt
                    => opt.MapFrom(src => src.Username));
        }

    }
}
