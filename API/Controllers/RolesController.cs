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
}