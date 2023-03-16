using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Security.Roles;

public class ListRoles
{
    public class ListRolesQuery : IRequest<List<IdentityRole>>
    {
        
    }

    public class ListRolesHandler : IRequestHandler<ListRolesQuery, List<IdentityRole>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public ListRolesHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<List<IdentityRole>> Handle(ListRolesQuery request, CancellationToken cancellationToken)
        {
            return await _roleManager.Roles.ToListAsync(cancellationToken);
        }
    }
}