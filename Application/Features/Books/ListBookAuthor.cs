using Application.Dtos;
using Application.Features.Books.BookSpecification;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Books;

public class ListBookAuthor
{
    public class ListBookAuthorQuery: IRequest<Result<List<BookDto>>>
    {
        
    }
    public class ListBookAuthorQueryHandler: IRequestHandler<ListBookAuthorQuery, Result<List<BookDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IListOfAuthorsService _authorsService;

        public ListBookAuthorQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,IListOfAuthorsService authorsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authorsService = authorsService;
        }
        public async Task<Result<List<BookDto>>> Handle(ListBookAuthorQuery request, CancellationToken cancellationToken)
        {
            var listBookSpec = new ListBookSpecification();
            var listBook = await _unitOfWork.Repository<Book>().ListWithSpecAsync(listBookSpec);
            var listBookSender = new List<BookDto>();
            
            foreach (var book in listBook)
            {
                var bookAuthDto = new BookDto()
                {
                    CategoryId = book.CategoryId,
                    Category = book.Category.Description,
                    Edition = book.Edition,
                    Price = book.Price,
                    Rating = book.Rating,
                    Synopsis = book.Synopsis,
                    Title = book.Title,
                    BookAmount = book.BookAmount,
                    CoverUrl = book.CoverUrl,
                    PagesNumbers = book.PagesNumbers,
                    PublishingCompany = book.PublishingCompany,
                    PublishingYear = book.PublishingYear,
                    ISBN = book.ISBN
                };
                var authorList = _authorsService.GetAuthors(book.Id);
                foreach (var  author  in authorList)
                {
                    bookAuthDto.Authors.Add(author.Author.FullName);
                }
                
                listBookSender.Add(bookAuthDto);
            }

            return listBookSender;
        }
    }
}
