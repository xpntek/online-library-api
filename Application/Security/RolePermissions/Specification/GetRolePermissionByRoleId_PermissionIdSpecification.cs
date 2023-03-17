using Application.Specifications;
using Domain;

namespace Application.Security.RolePermissions.Specification;

public class GetRolePermissionByRoleId_PermissionIdSpecification : BaseSpecification<RolePermission>
{
    public GetRolePermissionByRoleId_PermissionIdSpecification(string roleId, int permissionId) :
        base(x => x.RoleId == roleId && x.PermissionId == permissionId)
    {
        
    }
}