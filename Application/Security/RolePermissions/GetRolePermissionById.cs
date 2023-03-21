using Application.Errors;
using Application.Interfaces;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Security.RolePermissions;

public class GetRolePermissionById
{
    public class GetRolePermissionByIdQuery : IRequest<Result<RolePermission>>
    {
        public int Id { get; set; }
    }

    public class GetRolePermissionByIdHandler : IRequestHandler<GetRolePermissionByIdQuery, Result<RolePermission>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRolePermissionByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<RolePermission>> Handle(GetRolePermissionByIdQuery request, CancellationToken cancellationToken)
        {
            var rolePermission = await _unitOfWork.Repository<RolePermission>().GetByIdAsync(request.Id);

            if (rolePermission == null)
                return Results.NotFoundError("RolePermission");

            return rolePermission;
        }
    }

}