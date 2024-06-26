using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTService.Application.DTOs
{
    public class LoginResponseDTO
    {
        public int? Code { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }

        public LoginResponseDTO(int code, string message, string? token) {
            Code = code;
            Message = message;
            Token = token;
        }
    }
}
