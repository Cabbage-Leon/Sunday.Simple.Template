using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using Sunday.Simple.Template.Entity.Maps;

namespace Sunday.Simple.Template.Entity
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assemblies = GetCurrentPathAssembly()
                .Where(x => !x.GetName().Name.Equals("Sunday.Simple.Template.Entity"));
            foreach (var assembly in assemblies)
            {
                //找到所有实体类
                var entityTypes = assembly.GetTypes()
                    .Where(type => !string.IsNullOrWhiteSpace(type.Namespace))
                    .Where(type => type.IsClass)
                    .Where(type => type.Name != nameof(Entity))
                    .Where(type => type.BaseType != null)
                    .Where(type => typeof(ITrack).IsAssignableFrom(type));

                foreach (var entityType in entityTypes)
                {
                    if (modelBuilder.Model.FindEntityType(entityType) != null) continue;
                    modelBuilder.Model.AddEntityType(entityType);
                }
            }

            base.OnModelCreating(modelBuilder);
            
            #region 注册领域模型与数据库的映射关系

            modelBuilder.ApplyConfiguration(new SysUserMap());

            #endregion 注册领域模型与数据库的映射关系
        }

        public override int SaveChanges()
        {
            SetTrackInfo();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetTrackInfo();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetTrackInfo()
        {
            ChangeTracker.DetectChanges();

            //新增和更新的实体
            var entries = this.ChangeTracker.Entries()
                .Where(x => x.Entity is ITrack)
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var entry in entries)
            {
                var entity = entry.Entity;
                var entityBase = entity as ITrack;
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entityBase.UpdateModifyTime();
                        break;
                    case EntityState.Added:
                        entityBase.UpdateCreateTime();
                        break;
                }
            }
        }
        
        private List<Assembly> GetCurrentPathAssembly()
        {
            var dlls = DependencyContext.Default.CompileLibraries
                .Where(x => !x.Name.StartsWith("Microsoft") && !x.Name.StartsWith("System"))
                .ToList();
            var list = new List<Assembly>();
            if (dlls.Any())
            {
                foreach (var dll in dlls)
                {
                    if (dll.Type == "project")
                    {
                        list.Add(Assembly.Load(dll.Name));
                    }
                }
            }
            return list;
        }
    }
}