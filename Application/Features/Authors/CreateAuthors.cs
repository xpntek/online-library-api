using Application.Dtos;
using Application.Errors;
using Application.Features.Authors.AuthorSpecification;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;
using Persistence;

namespace Application.Features.Authors;

public class CreateAuthors
{
    public class CreateAuthorCommand : IRequest<Result<AuthorDto>>
    {
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string Biography { get; set; }
    }

    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Result<AuthorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAuthorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AuthorDto>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorSpec = new FoundAuthorByFullNameSpecification(request.FullName);
            var authorObject = await  _unitOfWork.Repository<Author>().GetEntityWithSpec(authorSpec);

            if (authorObject is not null)
            {
                return Results.ConflictError(" this author already");
            }
            
            var author = new Author()
            {
                FullName = request.FullName.ToLower(),
                Biography = request.Biography,
                Gender = request.Gender,
                Nationality = request.Nationality
            };

            _unitOfWork.Repository<Author>().Add(author);
            var result = await _unitOfWork.Complete() < 0;
            if (result)
            {
                return Results.InternalError("Author don't save");
            }

            return _mapper.Map<AuthorDto>(author);
        }
    }
}