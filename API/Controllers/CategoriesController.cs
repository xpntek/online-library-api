using API.Serialization;
using Application.Dtos;
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
    public async Task<IActionResult> SaveCategory(CreateCategory.CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return this.SerializeResult(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> ListCategory()
    {
        var result = await _mediator.Send(new ListCategory.ListCategoryQuery());
        return this.SerializeResult(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var result = await _mediator.Send(new ListCategoryById.GetCategoryByIdQuery{Id = id});
        return this.SerializeResult(result);
    }
    
    [HttpGet("description/{description}")]
    public async Task<IActionResult> GetCategoryByDescription(string description)
    {
        var result = await _mediator.Send(new ListCategoryByDescription.ListCategoryByDescriptionQuery{Description = description});
        return this.SerializeResult(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, UpdateCategory.UpdateCategoryCommand command)
    {
        command.Id = id;
        var result =await _mediator.Send(command);
        return this.SerializeResult(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await _mediator.Send(new DeleteCategory.DeleteCategoryCommand{Id =id });
        return this.SerializeResult(result);
    }
    
    
}