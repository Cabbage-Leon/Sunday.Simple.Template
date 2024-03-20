using AutoMapper;
using Sunday.Simple.Template.Model.Dto;
using Sunday.Simple.Template.Model.Entity;

namespace Repository.Extension.ServiceExtensions;

public class MapperCustomProfile : Profile
{
    public MapperCustomProfile()
    {
        CreateMap<SysUser, UserDto>();
        CreateMap<UserDto, SysUser>();
    }
}