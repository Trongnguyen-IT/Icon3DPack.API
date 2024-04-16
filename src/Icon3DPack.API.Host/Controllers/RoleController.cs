using Icon3DPack.API.Application.Models;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Application.Services.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Amazon.S3.Util.S3EventNotification;

namespace Icon3DPack.API.Host.Controllers;

public class RoleController : ApiController
{
    private readonly IRoleService _roleService;
    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _roleService.GetAll());
    }

    [HttpPost("Assign/{userId}/{roleId}")]
    public async Task<IActionResult> Assign(int userId, int roleId)
    {
        return Ok(await _roleService.Assign(userId, roleId));
    }
}
