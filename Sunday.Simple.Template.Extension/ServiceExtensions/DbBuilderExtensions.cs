using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sunday.Simple.Template.Common;
using Sunday.Simple.Template.Entity;

namespace Repository.Extension.ServiceExtensions
{
    public static class DbBuilderExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContextPool<EfContext>((provider, options) =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var dbSettings = configuration.GetSection(nameof(DbSettings)).Get<DbSettings>();

                options.UseNpgsql(dbSettings.ConnectionString,
                    opt => opt.CommandTimeout(dbSettings.CommandTimeout).EnableRetryOnFailure());

                if (dbSettings.EnableSensitiveDataLogging) options.EnableSensitiveDataLogging();
            });

            return services;
        }
    }
}
