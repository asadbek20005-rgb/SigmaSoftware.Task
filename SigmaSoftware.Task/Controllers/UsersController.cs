using Microsoft.AspNetCore.Mvc;
using SigmaSoftware.Common.Models;
using SigmaSoftware.Service.Extensions;
using SigmaSoftware.Service.ServiceContracts;

namespace SigmaSoftware.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] AddUserModel model)
    {
        await _userService.AddUser(model);
        if (_userService.IsValid)
        {
            return Ok("done");
        }

        _userService.CopyToModelState(ModelState);
        return BadRequest(ModelState);
    }


    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        if (_userService.IsValid)
        {
            return Ok(users);
        }
        _userService.CopyToModelState(ModelState);
        return BadRequest(ModelState);
    }


    [HttpPut]
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserModel model)
    {
        await _userService.UpdateUser(userId, model);
        if (_userService.IsValid)
            return Ok("done");  

        _userService.CopyToModelState(ModelState);
        return BadRequest(ModelState);
    }
}
