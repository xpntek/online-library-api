using Application.Errors;
using Application.Interfaces;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Categories;

public class ListCategoryById
{
    public class ListCategoryByIdQuery :IRequest<Result<Category>>
    {
        public int Id { get; set; }
    }
    public class ListCategoryByIdQueryHandler : IRequestHandler<ListCategoryByIdQuery,Result<Category>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Category>> Handle(ListCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(request.Id);
            if (category is null)
            {
                return Results.NotFoundError("Id: "+request.Id);
            }

            return category;
        }
    }
    
}