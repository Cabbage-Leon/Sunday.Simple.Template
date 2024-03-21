using Sunday.Simple.Template.Entity;
using Sunday.Simple.Template.Model.Dto;

namespace Sunday.Simple.Template.IService;

public interface IUserService
{
    Task<UserDto> Get(int id);
    Task Add(SysUser user);
}