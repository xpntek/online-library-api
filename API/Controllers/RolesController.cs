using Application.Security.Roles;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class RolesController : BaseApiController
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<Result<IdentityRole>> CreateRole(CreateRole.CreateRoleCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpGet]
    public async Task<List<IdentityRole>> ListPermission()
    {
        return await _mediator.Send(new ListRoles.ListRolesQuery());
    }

    [HttpDelete("{roleId}")]
    public async Task<Result<IdentityRole>> DeleteRole(string roleId)
    {
        return await _mediator.Send(new DeleteRole.DeleteRoleCommand {RoleId = roleId});
    }
    
    [HttpGet("{roleId}")]
    public async Task<Result<IdentityRole>> GetRoleById(string roleId)
    {
        return await _mediator.Send(new GetRoleById.GetRoleByIdQuery {RoleId = roleId});
    }

    [HttpPut("{roleId}")]
    public async Task<Result<IdentityRole>> UpdateRole(string roleId, UpdateRole.UpdateRoleCommand command)
    {
        command.RoleId = roleId;
        return await _mediator.Send(command);
    }
}