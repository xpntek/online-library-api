using Application.Interfaces;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Results = Application.Errors.Results;

namespace Application.Security.Permissions;

public class GetPermissionById
{
    public class GetPermissionByIdQery : IRequest<Result<Permission>>
    {
        public int Id { get; set; }
    }

    public class GetPermissionByIdHandler : IRequestHandler<GetPermissionByIdQery, Result<Permission>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPermissionByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Result<Permission>> Handle(GetPermissionByIdQery request, CancellationToken cancellationToken)
        {
            var permission = await _unitOfWork.Repository<Permission>().GetByIdAsync(request.Id);

            if (permission is null)
                return Results.NotFoundError("Permission");

            return permission;
        }
    }
}