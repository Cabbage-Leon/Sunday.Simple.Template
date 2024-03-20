using AutoMapper;
using Sunday.Simple.Template.Entity;
using Sunday.Simple.Template.Model.Dto;

namespace Repository.Extension.ServiceExtensions;

public class MapperCustomProfile : Profile
{
    public MapperCustomProfile()
    {
        CreateMap<SysUser, UserDto>();
        CreateMap<UserDto, SysUser>();
    }
}