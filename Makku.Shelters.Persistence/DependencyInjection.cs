using System.Text;
using Makku.Shelters.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Makku.Shelters.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SheltersDbContext>(opt =>
            {
                opt.UseNpgsql(Environment.GetEnvironmentVariable("DbConnection"));
            });
            services.AddScoped<ISheltersDbContext>(provider => provider.GetService<SheltersDbContext>());
            return services;
        }
        public static IServiceCollection AddAuthenticationPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SigningKey"])),
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["JwtSettings:Audiences:0"],
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                });
            return services;
        }
    }
}
