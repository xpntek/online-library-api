using Application.Specifications;
using Domain;

namespace Application.Security.Permissions.Specification;

public class GetPermissionByCodeSpecification : BaseSpecification<Permission>
{
    public GetPermissionByCodeSpecification(string code) : 
        base(x => x.Code == code)
    {
    }
}