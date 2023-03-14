using Application.Errors;
using Application.Interfaces;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Security.Permissions;

public class DeletePermission
{
    public class DeletePermissionCommand : IRequest<Result<Permission>>
    {
        public int Id { get; set; }
    }

    public class DeletePermissionHandler : IRequestHandler<DeletePermissionCommand, Result<Permission>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePermissionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Permission>> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await _unitOfWork.Repository<Permission>().GetByIdAsync(request.Id);

            if (permission is null)
                return Results.NotFoundError("Permission");
            
            _unitOfWork.Repository<Permission>().Delete(permission);

            return permission;
        }
    }
}