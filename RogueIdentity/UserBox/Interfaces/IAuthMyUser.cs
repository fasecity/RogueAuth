using Microsoft.AspNetCore.Identity;
using RogueIdentity.Models;
using System.Threading.Tasks;

namespace RogueIdentity.UserBox.Interfaces
{
    interface IAuthMyUser
    {
        Task<object> LoginAsync(LoginDto model);
        Task<object> RegisterAsync(RegisterDto model);
        Task<object> GenerateMyUserJwtToken(string email, IdentityUser user);
    }
}
