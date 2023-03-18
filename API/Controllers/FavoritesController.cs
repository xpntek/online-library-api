using API.Serialization;
using Application.Dtos;
using Application.Features.Favorites;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class FavoriteController : BaseApiController
{
    private readonly IMediator _mediator;

    public FavoriteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SaveFavorite(CreateFavorite.CreateFavoriteCommand command)
    {
        var result = await _mediator.Send(command);
        return this.SerializeResult(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> ListFavorite()
    {
        var result = await _mediator.Send(new ListFavorite.ListFavoriteQuery());
        return this.SerializeResult(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFavorite(int id)
    {
        var result = await _mediator.Send(new DeleteFavorite.DeleteFavoriteCommand{Id = id});
        return this.SerializeResult(result);
    }
}