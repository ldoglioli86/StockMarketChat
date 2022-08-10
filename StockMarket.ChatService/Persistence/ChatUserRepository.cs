using System.Transactions;
using StockMarket.Chat.Models;
using StockMarket.Chat.Persistence.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace StockMarket.Chat.Persistence
{
    public class ChatUserRepository : Repository<ChatUser>, IChatUserRepository
    {
        private readonly UserManager<ChatUser> _userManager;
        private readonly AppDbContext _context;

        public ChatUserRepository(UserManager<ChatUser> userManager, AppDbContext context)
        : base(context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<ChatUser> GetUserById(string id)
        {
            var userFrom = await _context.Users.AsNoTracking()
            .Select(user => new ChatUser
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                SecurityStamp = user.SecurityStamp,
                PasswordHash = user.PasswordHash
            })
            .FirstOrDefaultAsync(user => user.Id == id);

            return userFrom;

        }
        public async Task<IEnumerable<ChatUser>> GetUsers()
        {
            var users = await _userManager.Users.AsNoTracking()
            .Select(user => new ChatUser
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
                ,
                SecurityStamp = user.SecurityStamp
            }).ToListAsync();

            return users;
        }
        public async Task<ChatUser> CreateUser(ChatUser user, string password)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }
                scope.Complete();
            }
            return user;
        }

        public async Task<ChatUser> GetUserByUserName(string userName)
        {
            var userFrom = await _context.Users.AsNoTracking()
            .Select(user => new ChatUser
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                SecurityStamp = user.SecurityStamp,
                PasswordHash = user.PasswordHash
            })
            .FirstOrDefaultAsync(user => user.UserName == userName);

            return userFrom;
        }
    }
}

