using Application.Dtos;
using Application.Features.Books.BookSpecification;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;
using Persistence;
using Results = Application.Errors.Results;

namespace Application.Features.Books;

public class DeleteBookAuthors
{
    public class DeleteBookAuthorsCommand : IRequest<Result<BookDto>>
    {
        public int Id { get; set; }
    }

    public class DeleteBookAuthorsCommandHandler : IRequestHandler<DeleteBookAuthorsCommand, Result<BookDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public DeleteBookAuthorsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, DataContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<BookDto>> Handle(DeleteBookAuthorsCommand request, CancellationToken cancellationToken)
        {
            await using var transactionScope = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var bookObject = await _unitOfWork.Repository<Book>().GetByIdAsync(request.Id);
                var bookSpec = new FoundBookAuthorByBookIdSpecification(bookObject.Id);
                var bookAuthorList = await _unitOfWork.Repository<BookAuthor>().ListWithSpecAsync(bookSpec);

                foreach (var bookAuthor in bookAuthorList)
                {
                    _unitOfWork.Repository<BookAuthor>().Delete(bookAuthor);
                }
                
                var bookAuthDto = new BookDto()
                {
                    CategoryId =bookObject.CategoryId,
                    Category = bookObject.Category.Description,
                    Edition = bookObject.Edition,
                    Price =bookObject.Price,
                    Rating =bookObject.Rating,
                    Synopsis = bookObject.Synopsis,
                    Title =bookObject.Title,
                    BookAmount = bookObject.BookAmount,
                    CoverUrl = bookObject.CoverUrl,
                    PagesNumbers =bookObject.PagesNumbers,
                    PublishingCompany = bookObject.PublishingCompany,
                    PublishingYear = bookObject.PublishingYear,
                    ISBN = bookObject.ISBN
                };
                
                _unitOfWork.Repository<Book>().Delete(bookObject);
                var result = await _unitOfWork.Complete();
                if (result < 0)
                {
                    return Results.InternalError("Fail to delete book");
                }
                
                await transactionScope.CommitAsync(cancellationToken);
                return bookAuthDto;
            }

            catch (Exception e)
            {
                await transactionScope.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}