using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Loans;

public class UpdateEffectiveReturnDate
{
    public class UpdateEffectiveReturnDateCommand : IRequest<Result<LoanDto>>
    {
        public int Id { get; set; }
        public DateTimeOffset? EffectiveReturnDate { get; set; }
    }

    public class UpdateEffectiveReturnDateCommandHandler : IRequestHandler<UpdateEffectiveReturnDateCommand, Result<LoanDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEffectiveReturnDateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<LoanDto>> Handle(UpdateEffectiveReturnDateCommand request, CancellationToken cancellationToken)
        {
            var loan = await _unitOfWork.Repository<Loan>().GetByIdAsync(request.Id);
            if (loan is null)
            {
                return Results.NotFoundError("Id:" + request.Id);
            }
            
            loan.EffectiveReturnDate = request.EffectiveReturnDate;
            
            _unitOfWork.Repository<Loan>().Update(loan);
            var result = await _unitOfWork.Complete();
            if (result < 0)
            {
                return Results.InternalError("Error to update");
            }

            return _mapper.Map<LoanDto>(loan);
        }
    }
}