using API.Serialization;
using Application.Dtos;
using Application.Features.Loans;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class LoansController : BaseApiController
{
    private readonly IMediator _mediator;

    public LoansController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SaveLoan(CreateLoan.CreateLoanCommand command)
    {
        var result = await _mediator.Send(command);
        return this.SerializeResult(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> ListLoan()
    {
        var result =  await _mediator.Send(new ListLoan.ListLoanQuery());
        return this.SerializeResult(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLoanById(int id)
    {
        var result =  await _mediator.Send(new GetLoanById.GetLoanByIdQuery{Id = id});
        return this.SerializeResult(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLoan(int id, UpdateLoan.UpdateLoanCommand command)
    {
        command.Id=id;
        var result =  await _mediator.Send(command);
        return this.SerializeResult(result);
    } 
    
    [HttpPut("EffectiveReturnDate/{id}")]
    public async Task<IActionResult> UpdateEffectiveReturnDate(int id, UpdateEffectiveReturnDate.UpdateEffectiveReturnDateCommand command)
    {
        command.Id=id;
        var result =  await _mediator.Send(command);
        return this.SerializeResult(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLoan(int id)
    {
        var result =  await _mediator.Send(new DeleteLoan.DeleteLoanCommand{Id = id});
        return this.SerializeResult(result);
    }

}