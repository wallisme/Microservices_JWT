using JWTService.Application.DTOs;
using JWTService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTServiceTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserAction _userAction;
        public AccountController(IUserAction userAction)
        {
            _userAction = userAction;
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterDTO registerDTO)
        {
            var result = _userAction.Register(registerDTO).Result;
            return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var result = _userAction.Login(loginDTO).Result;
            return Ok(result);
        }
    }
}
