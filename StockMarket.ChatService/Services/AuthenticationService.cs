using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using StockMarket.Chat.Models;
using StockMarket.Chat.Persistence;
using StockMarket.Chat.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace StockMarket.Chat.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public AuthenticationService(IUnitOfWork unitOfWork, IConfiguration config) 
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<LoginResponse> Login(string username, string password)
        {
            LoginResponse result = new LoginResponse();
            List<string> err = new List<string>();

            try
            {
                //Search for user filtering by userName
                var user = await _unitOfWork.UserRepository.GetUserByUserName(username);

                if (user == null)
                {
                    err.Add("The user doesn't exists.");
                    result.errors = err;
                    return result;

                }

                var res = await _unitOfWork.AuthenticationRepository.CheckPasswordSignIn(user, password);

                if (!res.Succeeded)
                {
                    err.Add("Invalid password.");
                    result.errors = err;
                    return result;
                }

                var token = (await GenerateJwtToken(user));

                result.LoggedUser = user;

                if (result.LoggedUser != null)
                {
                    result.Token = token;

                }

                result.Successful = true;
            }
            catch (Exception ex)
            {
                err.Add("Error in AuthenticationService -> Login " + ex.Message);
                result.errors = err;
            }

            return result;
        }

        private async Task<string> GenerateJwtToken(ChatUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(300),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
