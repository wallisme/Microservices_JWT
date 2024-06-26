using JWTService.Application.DTOs;
using JWTService.Application.Interfaces;
using JWTService.Domain.Entities;
using JWTService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTService.Application.Implementations
{
    public class UserAction(ApplicationUserDbContext context, IConfiguration config) : IUserAction
    {
        private readonly ApplicationUserDbContext _context = context;
        private readonly IConfiguration _config = config;

        public async Task<LoginResponseDTO> Login(LoginDTO loginModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginModel.Email);
            if (user == null) return new LoginResponseDTO(-1, "User not found!!", null);
            var checkPassword = BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password);
            if (!checkPassword) return new LoginResponseDTO(-1, "Invalid Password!!", null);
            return new LoginResponseDTO(0, "Invalid Password!!", GenerateToken(user));
        }

        public Task<RegisterResponseDTO> Register(RegisterDTO user)
        {
            throw new NotImplementedException();
        }

        private string GenerateToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("PrivateKey").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Name)
            };
            var token = new JwtSecurityToken(
                issuer: _config.GetSection("Issuer").Value,
                audience: _config.GetSection("Audience").Value,
                claims: userClaims,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: credentials);
            return token.ToString();
        }
    }
}
