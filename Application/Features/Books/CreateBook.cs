using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Features.Books;

public class CreateBook
{
    public class CreateBookCommand : IRequest<Book>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string PublishingCompany { get; set; }
        public string ISBN { get; set; }
        public string PublishingYear { get; set; }
        public string Edition { get; set; }
        public string PagesNumbers { get; set; }
        public string Category  { get; set; }
        public string Synopsis { get; set; }
        public float Price { get; set; }
        public int BookAmount { get; set; }
        public string CoverUrl { get; set; }
        public int Rating { get; set; }
    }

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly IUnitOfWork _work;

        public CreateBookCommandHandler(IUnitOfWork work)
        {
            _work = work;
        }
        public Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
           
        }
    }
        
}