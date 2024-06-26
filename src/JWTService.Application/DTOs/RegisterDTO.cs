using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTService.Application.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required, Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }    
    }
}
