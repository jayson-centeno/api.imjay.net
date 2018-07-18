using Api.Imjay.Net;
using Api.Imjay.Net.Domain.Model.Authentication;
using Api.Imjay.Net.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JCLite.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            Configuration = configuration;
            _signInManager = signInManager;
        }


        public async Task<IActionResult> Index()
        {
            var result = await GetJwtSecurityToken(new UserAuthenticate() { Email = "jaysword1@yahoo.com", Password = "Sw0rdf!sh" });
            string token = new JwtSecurityTokenHandler().WriteToken(result);
            return View(
                new HomeModel() {
                    Token = token,
                    CaptchaKey =Configuration.GetValue<string>("CaptchaKey")
                });
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
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
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])), SecurityAlgorithms.HmacSha256)
            );
        }

        private async Task<ClaimsIdentity> GetIdentity(UserAuthenticate model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
                return new ClaimsIdentity(new System.Security.Principal.GenericIdentity(model.Email, "Token"), new Claim[] { });

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

        public IConfiguration Configuration { get; }

    }
}
