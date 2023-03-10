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
    public async Task<Result<Permission>> SaveBookAuthor(CreatePermission.CreatePermissionCommand command)
    {
        return await _mediator.Send(command);
    }
    
}