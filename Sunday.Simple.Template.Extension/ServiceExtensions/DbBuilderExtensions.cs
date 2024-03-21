using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sunday.Simple.Template.Common;

namespace Repository.Extension.ServiceExtensions
{
    public static class DbBuilderExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddRepository(options =>
            {
                var dbSettings = configuration.GetSection(nameof(DbSettings)).Get<DbSettings>();
                
                options.UseNpgsql(dbSettings!.ConnectionString,
                    opt => opt.CommandTimeout(dbSettings.CommandTimeout).EnableRetryOnFailure());

                if (dbSettings.EnableSensitiveDataLogging) options.EnableSensitiveDataLogging();
            });

            return services;
        }
    }
}
