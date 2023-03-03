using Application.Errors;
using Application.Features.Categories.Specification;
using Application.Interfaces;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Categories;

public class UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<Result<Category>>
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<Category>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Category>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(request.Id);
            if (category is null)
            {
                return Results.NotFoundError("" + request.Id);
            }

            category.Description = request.Description.ToLower();

            _unitOfWork.Repository<Category>().Update(category);
            var result = await _unitOfWork.Complete();

            if (result < 0)
            {
                return Results.InternalError(request.Description + " don't update");
            }

            return category;
        }
    }
}