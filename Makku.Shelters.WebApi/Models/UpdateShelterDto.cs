using AutoMapper;
using Makku.Shelters.Application.Commands.UpdateShelter;
using Makku.Shelters.Application.Common.Mappings;

namespace Makku.Shelters.WebApi.Models
{
    public class UpdateShelterDto : IMapWith<UpdateShelterCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateShelterDto, UpdateShelterCommand>()
                .ForMember(shelterCommand => shelterCommand.Id,
                    opt => opt.MapFrom(shelterDto => shelterDto.Id))
                .ForMember(shelterCommand => shelterCommand.Name,
                    opt => opt.MapFrom(shelterDto => shelterDto.Name));
        }
    }
}
