using Application.Specifications;
using Domain;

namespace Application.Features.Loans.Specification;

public class GetLoanByIdSpecification : BaseSpecification<Loan>
{
    public GetLoanByIdSpecification(int loanId) : base(loan => loan.Id==loanId )
    {
        AddInclude(loan => loan.Book );
        AddInclude(loan => loan.Client );
    }
}