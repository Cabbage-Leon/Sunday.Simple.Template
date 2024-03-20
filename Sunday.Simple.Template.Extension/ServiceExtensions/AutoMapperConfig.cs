using AutoMapper;

namespace Repository.Extension.ServiceExtensions;

public class AutoMapperConfig
{
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperCustomProfile());
        });
    }
}