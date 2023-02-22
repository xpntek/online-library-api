using Application.Interfaces;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Books;

public class ListBooks
{
    public class ListBooksQuery: IRequest<Result<List<Book>>>
    {
        
    }
    public class ListBookHandler: IRequestHandler<ListBooksQuery, Result<List<Book>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListBookHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<Book>>> Handle(ListBooksQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Repository<Book>().ListAllAsync();
            return result;
        }
    }
}