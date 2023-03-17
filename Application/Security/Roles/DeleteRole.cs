using Application.Errors;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Security.Roles;

public class DeleteRole
{
    public class DeleteRoleCommand : IRequest<Result<IdentityRole>>
    {
        public string RoleId { get; set; }
    }

    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, Result<IdentityRole>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public DeleteRoleHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<Result<IdentityRole>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId);

            if (role is null)
                return Results.NotFoundError("Role");

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
                return role;

            return Results.InternalError("Role");
        }
    }
}