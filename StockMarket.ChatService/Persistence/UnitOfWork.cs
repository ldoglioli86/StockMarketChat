using StockMarket.Chat.Models;
using StockMarket.Chat.Persistence.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace StockMarket.Chat.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ChatUser> _userManager;
        private readonly SignInManager<ChatUser> _signInManager;

        private ChatUserRepository _userRepository;
        private AuthenticationRepository _authenticationRepository;

        public UnitOfWork(AppDbContext context, UserManager<ChatUser> userManager, SignInManager<ChatUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IChatUserRepository UserRepository => _userRepository = _userRepository ?? new ChatUserRepository(_userManager, _context);
        public IAuthenticationRepository AuthenticationRepository => _authenticationRepository = _authenticationRepository ?? new           AuthenticationRepository(_signInManager, _context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
