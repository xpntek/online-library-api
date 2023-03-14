using Application.Interfaces;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Security.Permissions;

public class ListPermission
{
    public class ListPermissionQuery : IRequest<List<Permission>>
    {
        
    }

    public class ListPermissionHandler : IRequestHandler<ListPermissionQuery, List<Permission>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListPermissionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<List<Permission>> Handle(ListPermissionQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<Permission>().ListAllAsync();
        }
    }
}