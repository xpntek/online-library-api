using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class UserAccessor : IUserAccessor
{
    public readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentUserID()
    {
        var userId = _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        return userId;
    }
}