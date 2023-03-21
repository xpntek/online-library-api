using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Security.RolePermissions;

public class ListRolePermissions
{
    public class ListRolePermissionQuery : IRequest<List<RolePermission>>
    {
        
    }

    public class ListRolePermissionHandler : IRequestHandler<ListRolePermissionQuery, List<RolePermission>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListRolePermissionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<RolePermission>> Handle(ListRolePermissionQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<RolePermission>().ListAllAsync();
        }
    }
}