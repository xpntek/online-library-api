using Application.Errors;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Security.Roles;

public class CreateRole
{
    public class CreateRoleCommand : IRequest<Result<IdentityRole>>
    {
        public string Name { get; set; }
    }

    public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, Result<IdentityRole>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateRoleHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<Result<IdentityRole>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var roleExists = await _roleManager.RoleExistsAsync(request.Name);

            if (roleExists != true)
                return Results.ConflictError("Role");

            var role = new IdentityRole
            {
                Name = request.Name
            };

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
                return Results.InternalError();

            return role;
            
        }
    }
}