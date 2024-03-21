using Microsoft.AspNetCore.Mvc;
using Sunday.Simple.Template.IService;
using Sunday.Simple.Template.Model.Dto;

namespace Sunday.Simple.Template.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController(
    ILogger<UserController> logger,
    IUserService userService)
    : ControllerBase
{
    private readonly ILogger<UserController> _logger = logger;

    [HttpGet]
    public Task<UserDto> Get(int id)
    {
        return userService.Get(id);
    }
}