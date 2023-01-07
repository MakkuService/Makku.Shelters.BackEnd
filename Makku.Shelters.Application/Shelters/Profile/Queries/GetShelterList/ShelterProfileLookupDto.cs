using Makku.Shelters.Application.Common.Mappings;
using Makku.Shelters.Domain;

namespace Makku.Shelters.Application.Shelters.Profile.Queries.GetShelterList
{
    public class ShelterProfileLookupDto : IMapWith<ShelterProfile>
    {
        public Guid ShelterProfileId { get; set; }
        public string ShelterName { get; set; }
        public string? Address { get; set; }
        public string? Coordinate { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Description { get; set; }
        public decimal? CumulativeDonate { get; set; }
        public decimal? Donation { get; set; }
        public string? Problems { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public string? Inn { get; set; }
        public string? Ogrn { get; set; }
        public string? BankAccount { get; set; }
        public string? Ceo { get; set; }
        public decimal? SummDonation { get; set; }
        public DateTime? FoundDate { get; set; }
        public int? Subscribers { get; set; }
        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<ShelterProfile, ShelterProfileLookupDto>()
                .ForMember(shelterVm => shelterVm.ShelterName, opt => opt.MapFrom(shelter => shelter.ShelterName))
                .ForMember(shelterVm => shelterVm.ShelterProfileId,
                    opt => opt.MapFrom(shelter => shelter.ShelterProfileId))
                .ForMember(shelterVm => shelterVm.Address, opt => opt.MapFrom(shelter => shelter.Address))
                .ForMember(shelterVm => shelterVm.Coordinate, opt => opt.MapFrom(shelter => shelter.Coordinate))
                .ForMember(shelterVm => shelterVm.Email, opt => opt.MapFrom(shelter => shelter.Email))
                .ForMember(shelterVm => shelterVm.Phone, opt => opt.MapFrom(shelter => shelter.Phone))
                .ForMember(shelterVm => shelterVm.Description, opt => opt.MapFrom(shelter => shelter.Description))
                .ForMember(shelterVm => shelterVm.CumulativeDonate,
                    opt => opt.MapFrom(shelter => shelter.CumulativeDonate))
                .ForMember(shelterVm => shelterVm.Donation, opt => opt.MapFrom(shelter => shelter.Donation))
                .ForMember(shelterVm => shelterVm.Problems, opt => opt.MapFrom(shelter => shelter.Problems))
                .ForMember(shelterVm => shelterVm.CreatedOn, opt => opt.MapFrom(shelter => shelter.CreatedOn))
                .ForMember(shelterVm => shelterVm.ModifiedOn, opt => opt.MapFrom(shelter => shelter.ModifiedOn))
                .ForMember(shelterVm => shelterVm.IsActive, opt => opt.MapFrom(shelter => shelter.IsActive))
                .ForMember(shelterVm => shelterVm.Inn, opt => opt.MapFrom(shelter => shelter.Inn))
                .ForMember(shelterVm => shelterVm.Ogrn, opt => opt.MapFrom(shelter => shelter.Ogrn))
                .ForMember(shelterVm => shelterVm.BankAccount, opt => opt.MapFrom(shelter => shelter.BankAccount))
                .ForMember(shelterVm => shelterVm.Ceo, opt => opt.MapFrom(shelter => shelter.Ceo))
                .ForMember(shelterVm => shelterVm.SummDonation, opt => opt.MapFrom(shelter => shelter.SummDonation))
                .ForMember(shelterVm => shelterVm.FoundDate, opt => opt.MapFrom(shelter => shelter.FoundDate))
                .ForMember(shelterVm => shelterVm.Subscribers,
                    opt => opt.MapFrom(shelter => shelter.Subscribers));
        }
    }
}
