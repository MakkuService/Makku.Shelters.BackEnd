namespace Makku.Shelters.Domain
{
    public class ShelterProfile
    {
        private ShelterProfile(string identityId, string email, string shelterName)
        {
            IdentityId = identityId;
            Email = email;
            ShelterName = shelterName;
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = DateTime.UtcNow;
            IsActive = true;
        }

        public ShelterProfile() { }
        public Guid ShelterProfileId { get; set; }
        public string IdentityId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Email { get; set; }
        public string ShelterName { get; set; }
        public string? Address { get; set; }
        public string? Coordinate { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public decimal CumulativeDonate { get; set; }
        public decimal Donation { get; set; }
        public string? Problems { get; set; }
        public bool IsActive { get; set; }
        public string? Inn { get; set; }
        public string? Ogrn { get; set; }
        public string? BankAccount { get; set; }
        public string? Ceo { get; set; }
        public decimal SummDonation { get; set; }
        public DateTime? FoundDate { get; set; }
        public int? Subscribers { get; set; }

        public static ShelterProfile CreateShelterProfile(string identityId, string email, string shelterName)
            => new ShelterProfile(identityId, email, shelterName);
    }
}
