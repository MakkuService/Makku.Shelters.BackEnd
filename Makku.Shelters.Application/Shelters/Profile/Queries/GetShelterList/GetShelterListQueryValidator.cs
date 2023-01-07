using FluentValidation;

namespace Makku.Shelters.Application.Shelters.Profile.Queries.GetShelterList
{
    public class GetShelterListQueryValidator : AbstractValidator<GetShelterListQuery>
    {
        public GetShelterListQueryValidator()
        {
            //RuleFor(getShelterListQuery => getShelterListQuery.UserId).NotEqual(Guid.Empty).NotNull();
        }
    }
}
