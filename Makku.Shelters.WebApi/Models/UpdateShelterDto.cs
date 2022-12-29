using AutoMapper;
using Makku.Shelters.Application.Common.Mappings;
using Makku.Shelters.Application.Shelters.Profile.Commands.UpdateProfile;

namespace Makku.Shelters.WebApi.Models
{
    public class UpdateShelterDto : IMapWith<UpdateProfileCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateShelterDto, UpdateProfileCommand>()
                .ForMember(shelterCommand => shelterCommand.Id,
                    opt => opt.MapFrom(shelterDto => shelterDto.Id))
                .ForMember(shelterCommand => shelterCommand.Name,
                    opt => opt.MapFrom(shelterDto => shelterDto.Name));
        }
    }
}
