using MediatR;

namespace Makku.Shelters.Application.Shelters.Profile.Queries.GetShelterDetails
{
    public class GetShelterDetailsQuery : IRequest<ShelterDetailsVm>

    {
        public Guid Id { get; set; }
    }
}
