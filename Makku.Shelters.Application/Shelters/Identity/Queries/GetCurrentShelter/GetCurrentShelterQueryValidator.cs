using FluentValidation;

namespace Makku.Shelters.Application.Shelters.Identity.Queries.GetCurrentShelter
{
    public class GetCurrentShelterQueryValidator : AbstractValidator<GetCurrentShelterQuery>
    {
        public GetCurrentShelterQueryValidator()
        {
            RuleFor(q => q.ShelterProfileId).NotEqual(Guid.Empty);
        }
    }
}
