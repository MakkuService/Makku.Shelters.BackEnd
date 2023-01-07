using FluentValidation;

namespace Makku.Shelters.Application.Shelters.Profile.Queries.GetShelterDetails
{
    public class GetShelterDetailsQueryValidator : AbstractValidator<GetShelterDetailsQuery>
    {
        public GetShelterDetailsQueryValidator()
        {
            RuleFor(getShelterDetailsQuery => getShelterDetailsQuery.Id).NotEqual(Guid.Empty).NotNull();
        }
    }
}
