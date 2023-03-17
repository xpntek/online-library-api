using Application.Errors;
using Application.Interfaces;
using Application.Security.RolePermissions.Specification;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Security.RolePermissions;

public class CreateRolePermission
{
    public class CreateRolePermissionCommand : IRequest<Result<RolePermission>>
    {
        public string RoleId { get; set; }
        public int PermissionId { get; set; }
    }

    public class CreateRolePermissionHandler : IRequestHandler<CreateRolePermissionCommand, Result<RolePermission>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateRolePermissionHandler(IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
        }
        
        public async Task<Result<RolePermission>> Handle(CreateRolePermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await _unitOfWork.Repository<Permission>().GetByIdAsync(request.PermissionId);
            var role = await _roleManager.FindByIdAsync(request.RoleId);

            if (permission is null || role is null)
                return Results.NotFoundError("Permission and Role");

            var rolePermissionSpec = new GetRolePermissionByRoleId_PermissionIdSpecification(role.Id, permission.Id);

            var rolePermission = await _unitOfWork.Repository<RolePermission>().GetEntityWithSpec(rolePermissionSpec);

            if (rolePermission != null)
                return Results.ConflictError("RolePermission");
            
            rolePermission = new RolePermission
            {
                PermissionId = permission.Id,
                RoleId = role.Id
            };

            _unitOfWork.Repository<RolePermission>().Add(rolePermission);

            var result = await _unitOfWork.Complete() < 0;

            if (result)
                return Results.InternalError("RolePermission");

            return rolePermission;
        }
    }
}