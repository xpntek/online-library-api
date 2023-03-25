using Application.Specifications;
using Domain;

namespace Application.Features.Loans.Specification;

public class GetLoanByUserIdSpecification : BaseSpecification<Loan>
{ 
    public GetLoanByUserIdSpecification( string userId) : base( loan =>loan.Client.ApplicationUser.Id==userId )
    {
        AddInclude(loan => loan.Book );
        AddInclude(loan => loan.Client );
    }
}