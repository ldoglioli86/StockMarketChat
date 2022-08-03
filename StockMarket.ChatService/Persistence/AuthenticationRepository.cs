using StockMarket.Chat.Models;
using StockMarket.Chat.Persistence.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace StockMarket.Chat.Persistence
{
    public class AuthenticationRepository : Repository<ChatUser>, IAuthenticationRepository
    {
        private readonly SignInManager<ChatUser> _signInManager;

        public AuthenticationRepository(SignInManager<ChatUser> signInManager, AppDbContext context)
        : base(context)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> CheckPasswordSignIn(ChatUser user, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result;
        }
    }
}
