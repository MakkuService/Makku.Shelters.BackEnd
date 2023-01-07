using AutoMapper;
using Makku.Shelters.Application.Shelters.Profile.Queries.GetShelterDetails;
using Makku.Shelters.Persistence;
using Shelters.Tests.Common;
using Shouldly;

namespace Shelters.Tests.Shelters.Queries
{
    [Collection("QueryCollection")]
    public class GetShelterDetailsQueryHandlerTests
    {
        private readonly SheltersDbContext _context;
        private readonly IMapper _mapper;

        public GetShelterDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }


        [Fact]
        public async Task GetShelterDetailsQueryHandler_Success()
        {
            //Arreange
            var handler = new GetShelterDetailsQueryHandler(_context, _mapper);

            //Act
            var result = await handler.Handle(
                new GetShelterDetailsQuery
                {
                    Id = Guid.Parse("909F7C29-891B-4BE1-8504-21F84F262084")
                },
                CancellationToken.None);

            //Assert
            result.ShouldBeOfType<ShelterDetailsVm>();
            result.ShelterName.ShouldBe("Shelter2");
            result.CreatedOn.ShouldBe(DateTime.Today);
        }
    }
}
