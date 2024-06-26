using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTService.Application.DTOs
{
    public class RegisterResponseDTO(int code, string message)
    {
        public int? Code { get; set; } = code;
        public string? Message { get; set; } = message;
    }
}
