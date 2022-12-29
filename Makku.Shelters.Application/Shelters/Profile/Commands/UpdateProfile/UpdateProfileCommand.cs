using MediatR;

namespace Makku.Shelters.Application.Shelters.Profile.Commands.UpdateProfile
{
    public class UpdateProfileCommand:IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Coordinate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public decimal CumulativeDonate { get; set; }
        public decimal Donation { get; set; }
        public string Problems { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public string Inn { get; set; }
        public string Ogrn { get; set; }
        public string BankAccount { get; set; }
        public string Ceo { get; set; }
        public decimal SummDonation { get; set; }
        public DateTime FoundDate { get; set; }
        public int NumberOfPeople { get; set; }
    }
}

