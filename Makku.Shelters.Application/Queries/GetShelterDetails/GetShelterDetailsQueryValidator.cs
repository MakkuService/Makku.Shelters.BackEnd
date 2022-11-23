using FluentValidation;

namespace Makku.Shelters.Application.Queries.GetShelterDetails
{
    public class GetShelterDetailsQueryValidator:AbstractValidator<GetShelterDetailsQuery>
    {
        public GetShelterDetailsQueryValidator()
        {
            RuleFor(getShelterDetailsQuery => getShelterDetailsQuery.UserId).NotEqual(Guid.Empty).NotNull();
            RuleFor(getShelterDetailsQuery => getShelterDetailsQuery.Id).NotEqual(Guid.Empty).NotNull();
        }
    }
}
