using Application.Errors;
using Application.Interfaces;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Security.Permissions;

public class UpdatePermission
{
    public class UpdatePermissionCommand : IRequest<Result<Permission>>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class UpdatePermissionHandler : IRequestHandler<UpdatePermissionCommand, Result<Permission>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePermissionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Permission>> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await _unitOfWork.Repository<Permission>().GetByIdAsync(request.Id);

            if (permission is null)
                return Results.NotFoundError("Permission");
            
            permission.Description = request.Description;
            permission.Code = request.Code;

            _unitOfWork.Repository<Permission>().Update(permission);

            var success = await _unitOfWork.Complete() > 0;

            return success ? permission : permission;
        }
    }
}