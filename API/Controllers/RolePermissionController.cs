using Application.Security.RolePermissions;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class RolePermissionController : BaseApiController
{
    private readonly IMediator _mediator;

    public RolePermissionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<Result<RolePermission>> CreateRolePermission(CreateRolePermission.CreateRolePermissionCommand command)
    {
        return await _mediator.Send(command);
    }
}