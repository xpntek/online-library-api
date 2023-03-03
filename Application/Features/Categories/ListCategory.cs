using Application.Interfaces;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Categories;

public class ListCategory
{
    public class ListCategoryQuery : IRequest<Result<List<Category>>>
    {
    }

    public class ListCategoryQueryHandler : IRequestHandler<ListCategoryQuery, Result<List<Category>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListCategoryQueryHandler( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<Category>>> Handle(ListCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.Repository<Category>().ListAllAsync();
            return categories;
        }
    }
}