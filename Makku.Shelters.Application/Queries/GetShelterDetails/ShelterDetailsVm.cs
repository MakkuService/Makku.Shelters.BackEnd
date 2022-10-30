using AutoMapper;
using Makku.Shelters.Application.Common.Mappings;
using Makku.Shelters.Domain;

namespace Makku.Shelters.Application.Queries.GetShelterDetails
{
    public class ShelterDetailsVm : IMapWith<Shelter>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Shelter, ShelterDetailsVm>()
                .ForMember(shelterVm => shelterVm.Name, opt => opt.MapFrom(shelter => shelter.Name))
                .ForMember(shelterVm => shelterVm.CreatedOn, opt => opt.MapFrom(shelter => shelter.CreateOn))
                .ForMember(shelterVm => shelterVm.Id, opt => opt.MapFrom(shelter => shelter.Id));
        }


    }
}
