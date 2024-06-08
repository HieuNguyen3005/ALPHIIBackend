using Microsoft.AspNetCore.Identity;

namespace ALPHII.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
