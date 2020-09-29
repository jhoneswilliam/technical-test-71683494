using Data.Context;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection, IConfiguration configuration)
        {

            serviceCollection.AddDbContext<UnityOfWork>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"), b =>
                {
                    b.MigrationsAssembly("Api");
                });
            });

            serviceCollection.AddScoped<Repository>();
        }
    }
}
