using Application.Specifications;
using Domain;

namespace Application.Features.Favorites;

public class ListFavoriteSpecification : BaseSpecification<Favorite>
{
    public ListFavoriteSpecification( string id) :base( favorite => favorite.Client.ApplicationUser.Id==id)
    {
        AddInclude(favorite => favorite.Book);
        AddInclude(favorite => favorite.Client);
        
    }
}