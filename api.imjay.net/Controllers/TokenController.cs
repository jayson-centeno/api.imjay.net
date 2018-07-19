using Api.Imjay.Net.Domain.Model.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Imjay.Net
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        public IConfiguration Configuration { get; }

        public TokenController(IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            Configuration = configuration;
            _signInManager = signInManager;
        }

        [EnableCors("*")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserAuthenticate auth)
        {
            try
            {
                var token = await GetJwtSecurityToken(auth);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        private async Task<JwtSecurityToken> GetJwtSecurityToken(UserAuthenticate user)
        {
            var identity = await GetIdentity(user);
            if (identity == null)
                throw new UnauthorizedAccessException();

            return new JwtSecurityToken(
                issuer: Configuration["Tokens:Issuer"],
                audience: Configuration["Tokens:Issuer"],
                claims: GetTokenClaims(user, Configuration),
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])), SecurityAlgorithms.HmacSha256)
            );
        }

        private async Task<ClaimsIdentity> GetIdentity(UserAuthenticate model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
                return new ClaimsIdentity(new System.Security.Principal.GenericIdentity(model.Email, "Token"), new Claim[]{ });

            return null;
        }

        private static IEnumerable<Claim> GetTokenClaims(UserAuthenticate user, IConfiguration Configuration)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };
        }
    }
}