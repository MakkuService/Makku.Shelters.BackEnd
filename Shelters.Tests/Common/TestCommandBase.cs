using AutoMapper;
using Makku.Shelters.Application.Common.Mappings;
using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Shelters.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly SheltersDbContext Context;
        public IMapper Mapper;
        protected UserManager<IdentityUser>? UserManager;
        protected IConfiguration Configuration;


        protected TestCommandBase()
        {
            Context = SheltersContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(ISheltersDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();

            var users = new List<IdentityUser> { new("User1"), new("User2") };
            UserManager = MockUserManager<IdentityUser>(users).Object;

            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings_test.json", optional: false)
                .Build();
        }

        public void Dispose() => SheltersContextFactory.Destroy(Context);

        private static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }



    }
}
