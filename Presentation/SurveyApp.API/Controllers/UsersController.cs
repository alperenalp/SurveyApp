using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SurveyApp.API.Models;
using SurveyApp.DTOs.Requests;
using SurveyApp.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(CreateNewUserRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetUserByEmailAsync(request.Email);
                if (user == null)
                {
                    await _userService.CreateUserAsync(request);
                    return Ok();
                }
                ModelState.AddModelError("", "Bu email kullanılmaktadır.");
            }
            return BadRequest(ModelState);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserLoginVM userLoginVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.ValidateUserAsync(userLoginVM.Username, userLoginVM.Password);
                if (user != null)
                {
                    Claim[] claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Kirilmasi-Imkansiz-Cok-Ama-Cok-Gizli-Keyim"));
                    var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: "server",
                        audience: "client",
                        claims: claims,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: credential
                        );

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
                ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış!");
            }
            return BadRequest(ModelState);
        }
    }
}
