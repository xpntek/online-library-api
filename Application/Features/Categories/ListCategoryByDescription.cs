using Application.Errors;
using Application.Features.Categories.Specification;
using Application.Interfaces;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Categories;

public class ListCategoryByDescription
{
    public class ListCategoryByDescriptionQuery : IRequest<Result<Category>>
    {
        public string Description { get; set; }
    }

    public class ListCategoryByDescriptionQueryHandler : IRequestHandler<ListCategoryByDescriptionQuery, Result<Category>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListCategoryByDescriptionQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Category>> Handle(ListCategoryByDescriptionQuery request,
            CancellationToken cancellationToken)
        {
            var categorySpec = new FoundCategoryByDescriptionSpecification(request.Description);
            var category = await _unitOfWork.Repository<Category>().GetEntityWithSpec(categorySpec);
            if (category is null)
            {
                return Results.NotFoundError("Description: " + request.Description);
            }

            return category;
        }
    }
}