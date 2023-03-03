using Application.Errors;
using Application.Interfaces;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Categories;

public class DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<Result<Category>>
    {
        public int Id { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<Category>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        
        }

        public async Task<Result<Category>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(request.Id);
            if (category is null)
            {
                return Results.NotFoundError(""+request.Id);
            }
            
            _unitOfWork.Repository<Category>().Delete(category);
            var result = await _unitOfWork.Complete();
            if (result < 0)
            {
                return Results.InternalError(request.Id + " don't delete");
            }

            return category;
        }
    }
}