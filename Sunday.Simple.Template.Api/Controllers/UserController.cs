using Microsoft.AspNetCore.Mvc;
using Sunday.Simple.Template.IService;
using Sunday.Simple.Template.Model.Dto;

namespace Sunday.Simple.Template.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger,
        IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }
    
    [HttpGet]
    public Task<UserDto> Get(int id)
    {
        return _userService.Get(id);
    }
}