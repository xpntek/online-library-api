using Application.Errors;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Security.Roles;

public class UpdateRole
{
    public class UpdateRoleCommand : IRequest<Result<IdentityRole>>
    {
        public string RoleId { get; set; }
        public string Name { get; set; }
    }

    public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, Result<IdentityRole>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UpdateRoleHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<Result<IdentityRole>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId);

            if (role is null)
                return Results.NotFoundError("Role");

            role.Name = request.Name;

            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
                return role;

            return Results.InternalError("Role");
        }
    }
}