using Application.Security.Permissions;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PermissionContoller : BaseApiController
{
    private readonly IMediator _mediator;

    public PermissionContoller(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<Result<Permission>> CreatePermission(CreatePermission.CreatePermissionCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<Result<Permission>> DeletePermission(int id)
    {
        return await _mediator.Send(new DeletePermission.DeletePermissionCommand { Id = id });
    }

    [HttpGet]
    public async Task<List<Permission>> ListPermission()
    {
        return await _mediator.Send(new ListPermission.ListPermissionQuery());
    }

    [HttpPut("{id}")]
    public async Task<Result<Permission>> UpdatePermission(UpdatePermission.UpdatePermissionCommand command, int id)
    {
        command.Id = id;

        return await _mediator.Send(command);
    }

    [HttpGet("{id}")]
    public async Task<Result<Permission>> GetPermissionById(int id)
    {
        return await _mediator.Send(new GetPermissionById.GetPermissionByIdQery { Id = id });
    }
}