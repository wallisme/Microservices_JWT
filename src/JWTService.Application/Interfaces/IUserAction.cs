using JWTService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTService.Application.Interfaces
{
    public interface IUserAction
    {
        Task<LoginResponseDTO> Login(LoginDTO loginModel);
        Task<RegisterResponseDTO> Register(RegisterDTO user);
    }
}
