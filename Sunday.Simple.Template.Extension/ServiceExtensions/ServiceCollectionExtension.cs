using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sunday.Simple.Template.Entity;
using Sunday.Simple.Template.Repository.Base;
using Sunday.Simple.Template.Repository;

namespace Repository.Extension.ServiceExtensions;

 public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 使用默认的EfContext
        /// </summary>
        public static IServiceCollection AddRepository(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<EfContext>(options);
            services.AddScoped<DbContext, EfContext>();
            services.AddRepository();
            return services;
        }

        /// <summary>
        /// 使用自定义的Context,不过需要继承EfContext
        /// </summary>
        public static IServiceCollection AddRepository<TDbContext>(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options) where TDbContext : EfContext
        {
            services.AddDbContext<TDbContext>(options);
            services.AddScoped<DbContext, TDbContext>();
            services.AddRepository();
            return services;
        }

        /// <summary>
        /// 仓储的注册
        /// </summary>
        private static IServiceCollection AddRepository(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetCurrentPathAssembly().Where(x => !x.GetName().Name!.Equals("Sunday.Simple.Template.Repository"));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddRepository(assemblies, typeof(IRepository<>));
            services.AddRepository(assemblies, typeof(IRepository<,>));
            return services;
        }

        /// <summary>
        /// 将实现了IRepository接口的仓储，注册进容器
        /// IRepository<> => ImplementedClass => Interface 
        /// </summary>
        private static void AddRepository(this IServiceCollection services, IEnumerable<Assembly> assemblies, Type baseType)
        {
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes()
                                    .Where(x => x is { IsClass: true, IsAbstract: false, BaseType: not null }
                                                && x.HasImplementedRawGeneric(baseType));
                foreach (var type in types)
                {
                    var interfaces = type.GetInterfaces();
                    //如果有UserRepository实现接口IUserRepository，优先注册这对，否则注册自己
                    var interfaceType = interfaces.FirstOrDefault(x => x.Name == $"I{type.Name}") ?? type;
                    var serviceDescriptor = new ServiceDescriptor(interfaceType, type, ServiceLifetime.Transient);
                    if (!services.Contains(serviceDescriptor)) services.Add(serviceDescriptor);
                }
            }
        }
    }