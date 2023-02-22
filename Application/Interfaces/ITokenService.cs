using Domain;

namespace Application.Interfaces;

public interface ITokenService
{
    string CreateToken(ApplicationUser user);
}