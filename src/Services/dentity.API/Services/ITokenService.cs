using Authentication.Tokens;
using Identity.API.Models;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public interface ITokenService
    {
        Task<CustomToken> GetToken(TokenRequestModel model);
    }
}