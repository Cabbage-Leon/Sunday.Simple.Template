using AutoMapper;
using Sunday.Simple.Template.Entity;
using Sunday.Simple.Template.IService;
using Sunday.Simple.Template.Model.Dto;
using Sunday.Simple.Template.Repository.Base;

namespace Sunday.Simple.Template.Service;

public class UserService(IRepository<SysUser, int> repository, IMapper mapper) : IUserService
{
    public async Task<UserDto> Get(int id)
    {
        var model = await repository.GetAsync(id);
        return mapper.Map<UserDto>(model);
    }

    public async Task Add(SysUser user)
    {
        await repository.InsertAsync(user);
    }
}