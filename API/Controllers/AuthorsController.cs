using API.Serialization;
using Application.Dtos;
using Application.Features.Authors;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Results = Application.Errors.Results;

namespace API.Controllers;

public class AuthorsController : BaseApiController
{
    private readonly IMediator _mediator;

    public AuthorsController( IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> SaveAuthors( CreateAuthors.CreateAuthorCommand command)
    {
        var result = await _mediator.Send(command);
        return this.SerializeResult(result);
    }
    
    [HttpGet]
    public async Task<IActionResult>  ListAuthors()
    {
        var result = await _mediator.Send( new ListAllAuthors.ListAllAuthorsQuery());
        return this.SerializeResult(result);
     
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorsById( int id)
    {
        var result = await _mediator.Send(new ListAuthorById.ListAuthorByIdQuery{Id = id});
        return this.SerializeResult(result);
    }

    [HttpGet("fullname/{fullname}")]
    public async Task<IActionResult>  GetAuthorsByFullName( string fullname)
    {
        var result =await _mediator.Send(new GetAuthorByFullName.GetAuthorByFullNameQuery{FullName = fullname});
        return this.SerializeResult(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult>  DeleteAuthor(int id)
    {
        var result = await _mediator.Send(new DeleteAuthor.DeleteAuthorCommand { Id = id });
        return this.SerializeResult(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult>  UpdateAuthors(int id, UpdateAuthor.UpdateAuthorCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return this.SerializeResult(result);
    }

    


    
}