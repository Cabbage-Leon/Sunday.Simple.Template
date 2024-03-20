using AutoMapper;
using Sunday.Simple.Template.Entity;
using Sunday.Simple.Template.IService;
using Sunday.Simple.Template.Model.Dto;
using Sunday.Simple.Template.Repository.Base;
using Sunday.Simple.Template.Service.Base;

namespace Sunday.Simple.Template.Service;

public class UserService : BaseServices<SysUser, UserDto>, IUserService
{
    public UserService(IMapper mapper, IBaseRepository<SysUser> baseRepository) : base(mapper, baseRepository)
    {
    }

    public Task<UserDto> Get(int id)
    {
        throw new NotImplementedException();
    }
}