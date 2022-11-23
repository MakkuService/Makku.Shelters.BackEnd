using FluentValidation;

namespace Makku.Shelters.Application.Queries.GetShelterList
{
    public class GetShelterListQueryValidator : AbstractValidator<GetShelterListQuery>
    {
        public GetShelterListQueryValidator()
        {
            RuleFor(getShelterListQuery => getShelterListQuery.UserId).NotEqual(Guid.Empty).NotNull();
        }
    }
}
