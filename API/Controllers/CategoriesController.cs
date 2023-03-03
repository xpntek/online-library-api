using Application.Features.Books;
using Application.Features.Categories;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CategoriesController : BaseApiController
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task<Result<Category>> SaveCategory(CreateCategory.CreateCategoryCommand command)
    {
        return _mediator.Send(command);
    }
    
    [HttpGet]
    public Task<Result<List<Category>>> ListCategory()
    {
        return _mediator.Send(new ListCategory.ListCategoryQuery());
    }
    
    [HttpGet("{id}")]
    public Task<Result<Category>> ListCategoryById(int id)
    {
        return _mediator.Send(new ListCategoryById.ListCategoryByIdQuery{Id = id});
    }
    
    [HttpGet("description/{description}")]
    public Task<Result<Category>> ListCategoryByDescription(string description)
    {
        return _mediator.Send(new ListCategoryByDescription.ListCategoryByDescriptionQuery{Description = description});
    }
    
    [HttpPut("{id}")]
    public Task<Result<Category>> UpdateCategory(int id, UpdateCategory.UpdateCategoryCommand command)
    {
        command.Id = id;
        return _mediator.Send(command);
    }
    
    [HttpDelete("{id}")]
    public Task<Result<Category>> DeleteCategory(int id)
    {
        return _mediator.Send(new DeleteCategory.DeleteCategoryCommand{Id =id });
    }
    
    
}