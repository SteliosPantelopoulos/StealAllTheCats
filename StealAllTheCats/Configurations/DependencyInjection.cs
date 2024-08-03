using Microsoft.EntityFrameworkCore;
using StealAllTheCats.Domain.Interfaces;
using StealAllTheCats.Domain.Mappings;
using StealAllTheCats.Domain.Services;
using StealAllTheCats.Domain.Shared.Helpers;
using StealAllTheCats.Infrastructure.Context;
using StealAllTheCats.Infrastructure.Interfaces;
using StealAllTheCats.Infrastructure.Repositories;

namespace StealAllTheCats.Configurations;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        #region DB Contexts
        services.AddDbContext<DBContext>(options => options.UseSqlServer(configuration.GetConnectionString("DB")));
        #endregion DB Contexts

        #region Services
        services.AddScoped<ICatsService, CatsService>();
        #endregion Services

        #region Repositories
        services.AddScoped<ICatsRepository, CatsRepository>();
        #endregion Repositories

        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<HttpHelper>();
    }
}
