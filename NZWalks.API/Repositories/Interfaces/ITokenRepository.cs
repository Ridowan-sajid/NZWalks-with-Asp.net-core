using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
