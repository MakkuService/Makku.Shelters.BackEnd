using Makku.Shelters.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Makku.Shelters.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<SheltersDbContext>(opt =>
            {
                opt.UseNpgsql(connectionString);
            });
            services.AddScoped<ISheltersDbContext>(provider => provider.GetService<SheltersDbContext>());
            return services;
        }
    }
}
