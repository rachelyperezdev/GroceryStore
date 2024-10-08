using Backend.Core.Application.Interfaces.Repositories;
using Backend.Infrastructure.Persistence.Contexts;
using Backend.Infrastructure.Persistence.Interceptor;
using Backend.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infrastructure.Persistence.IoC
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<AuditableInterceptor>();

            #region Contexts
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options =>
                {
                    options.UseInMemoryDatabase("ApplicationDb");
                });
            }
            else
            {
                services.AddDbContext<ApplicationContext>((sp, options) =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("Default"),
                                         m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName))
                           .AddInterceptors(sp.GetService<AuditableInterceptor>()!);
                });
            }
            #endregion

            #region Repositories
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            #endregion
        }
    }
}
