using Sunday.Simple.Template.Entity;
using Sunday.Simple.Template.IService.Base;
using Sunday.Simple.Template.Model.Dto;

namespace Sunday.Simple.Template.IService;

public interface IUserService : IBaseService<SysUser, UserDto>
{
    Task<UserDto> Get(int id);
}