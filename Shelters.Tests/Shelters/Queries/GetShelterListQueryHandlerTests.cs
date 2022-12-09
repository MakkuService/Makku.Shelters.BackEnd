using AutoMapper;
using Makku.Shelters.Application.Queries.GetShelterList;
using Makku.Shelters.Domain;
using Makku.Shelters.Persistence;
using Shelters.Tests.Common;
using Shouldly;

namespace Shelters.Tests.Shelters.Queries
{
    [Collection("QueryCollection")]
    public class GetShelterListQueryHandlerTests
    {
        private readonly SheltersDbContext _context;
        private readonly IMapper _mapper;

        public GetShelterListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetShelterListQueryHandler_Success()
        {
            //Arreange
            var handler = new GetShelterListQueryHandler(_context, _mapper);

            //Act
            var result = await handler.Handle(
                new GetShelterListQuery
                {
                    UserId = SheltersContextFactory.UserBId
                },
                CancellationToken.None);

            //Assert
            result.ShouldBeOfType<ShelterListVm>();
            result.Shelters.Count.ShouldBe(2);
        }

    }
}
