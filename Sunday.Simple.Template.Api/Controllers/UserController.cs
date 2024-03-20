using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sunday.Simple.Template.IService;
using Sunday.Simple.Template.Model.Dto;

namespace Sunday.Simple.Template.Api.Controllers;

/// <summary>
/// 用户管理
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
[Authorize("Permission")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger,
        IHttpContextAccessor httpContextAccessor, 
        IUserService userService)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
    }
    
    [HttpGet]
    public Task<UserDto> Get(int id)
    {
        return _userService.Get(id);
    }
}