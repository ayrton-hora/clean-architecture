using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

using CleanArch.Domain.Account;
using CleanArchMvc.API.Models;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IAuthenticate _authentication;

        public TokensController(IAuthenticate authentication)
        {
            _authentication = authentication ?? throw new NullReferenceException(nameof(authentication));
        }

        [Authorize]
        // [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] RegisterModel registerModel)
        {
            if (await _authentication.RegisterUserAsync(registerModel.Email, registerModel.Password))
                return Ok($"User {registerModel.Email} was create successfully");

            else return BadRequest("Error on create a new user");
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel loginModel)
        {
            if (await _authentication.AuthenticateAsync(loginModel.Email, loginModel.Password))
                return GenerateToken(loginModel);

            else return BadRequest("Invalid login attempt");
        }

        private UserToken GenerateToken(LoginModel loginInfo)
        {
            Claim[] claims = new[]
            {
                new Claim("email", loginInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Private key
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(
                Environment.GetEnvironmentVariable("CLEAN_ARCH_KEY") ?? 
                    throw new NullReferenceException("Private security key is not configured")));

            // Digital signature
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            // Expiration time
            DateTime expiration = DateTime.UtcNow.AddMinutes(10);

            JwtSecurityToken token = new(
                issuer: Environment.GetEnvironmentVariable("CLEAN_ARCH_ISSUER") ?? throw new NullReferenceException("Issuer is not configured"),
                audience: Environment.GetEnvironmentVariable("CLEAN_ARCH_AUDIENCE") ?? throw new NullReferenceException("Audience is not configured"),
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
            };
        }
    }
}
