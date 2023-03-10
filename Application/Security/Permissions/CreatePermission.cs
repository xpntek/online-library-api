using System.Net;
using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using Application.Security.Permissions.Specification;
using Domain;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Security.Permissions;

public class CreatePermission
{
    public class CreatePermissionCommand : IRequest<Result<Permission>>
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
    
    public class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
    {
        public CreatePermissionCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }

    public class CreatePermissionHandler : IRequestHandler<CreatePermissionCommand, Result<Permission>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePermissionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Result<Permission>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var spec = new GetPermissionByCodeSpecification(request.Code);
            var permission = await _unitOfWork.Repository<Permission>().GetEntityWithSpec(spec);

            if (permission is not null)
                return Results.ConflictError("Permission");
           
            permission = new Permission()
            {
                Code = request.Code,
                Description = request.Description
            };

            _unitOfWork.Repository<Permission>().Add(permission);

            var success = await _unitOfWork.Complete() > 0;

            if (success) 
                return permission;

            return Results.InternalError("An error occurred!");
        }
    }
}