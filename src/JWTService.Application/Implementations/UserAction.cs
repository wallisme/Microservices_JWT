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
            return new LoginResponseDTO(0, "Successfull!!", GenerateToken(user));
        }

        public async Task<RegisterResponseDTO> Register(RegisterDTO registerModel)
        {
            if (registerModel == null) return new RegisterResponseDTO(-1, "User is null");
            var user = _context.Users.FirstOrDefaultAsync(x => x.Email == registerModel.Email).Result;
            if(user != null) return new RegisterResponseDTO(-1, "Email has been used !!!");
            var newUser = new ApplicationUser()
            {
                Name = registerModel.Name,
                Email = registerModel.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerModel.Password)
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();  
            return new RegisterResponseDTO(0, "Successfull !!!");
        }

        private string GenerateToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("PrivateKey").Value!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new Claim[]
            {   
                new (ClaimTypes.NameIdentifier, user.Id!),
                new (ClaimTypes.Email, user.Email!),
                new (ClaimTypes.NameIdentifier, user.Name!)
            };
            var token = new JwtSecurityToken(
                issuer: _config.GetSection("Issuer").Value,
                audience: _config.GetSection("Audience").Value,
                claims: userClaims,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
