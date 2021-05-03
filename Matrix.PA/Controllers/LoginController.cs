using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Matrix.PA.Models;
using Matrix.PA.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Matrix.PA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IAuthenticationService _authService;

        public LoginController(IAuthenticationService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        public IActionResult Token(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if(_authService.TrySignIn(model.Login, model.Password, out var userModel))
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, userModel.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, userModel.IsAdmin ? "Admin" : "User")
                    };

                    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(AuthenticationService.SecurityKey));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: "Matrix.PA",
                        audience: "Postman",
                        claims: claims.ToArray(),
                        signingCredentials: credentials,
                        expires: DateTime.Now.AddMinutes(30)
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(tokenString);
                }
                return Unauthorized();
            }
            return BadRequest();
        }
    }
}
