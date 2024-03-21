using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sunday.Simple.Template.Entity.Maps;

public class SysUserMap : IEntityTypeConfiguration<SysUser>
{
    public void Configure(EntityTypeBuilder<SysUser> builder)
    {
        // table
        builder.ToTable("sys_user");

        // keys
        builder.HasKey(t => t.Id);
        builder.Property(p => p.Id).HasColumnName("id").IsRequired();
        builder.Property(p => p.UserName).HasColumnName("user_name");
        builder.Property(p => p.Password).HasColumnName("password");
        builder.Property(p => p.EMail).HasColumnName("email");
        builder.Property(p => p.IsDelete).HasColumnName("is_delete");
        builder.Property(p => p.CreateTime).HasColumnName("create_time");
        builder.Property(p => p.UpdateTime).HasColumnName("update_time");
    }
}