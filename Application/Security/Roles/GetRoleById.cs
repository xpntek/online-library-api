using Application.Errors;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Security.Roles;

public class GetRoleById
{
    public class GetRoleByIdQuery : IRequest<Result<IdentityRole>>
    {
        public string RoleId { get; set; }
    }
    public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, Result<IdentityRole>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetRoleByIdHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<Result<IdentityRole>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId);

            if (role is null)
                return Results.NotFoundError("Role");

            return role;
        }
    }
}