using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AppLojaBackofficeApi.Models;
using AppLojaBackofficeMvc.Models;
using AppLojaBackofficeMvc.Services;
using Azure.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AppLojaBackofficeApi.Controllers
{
    [ApiController]
    [Route("api/conta")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IVendedorService _vendedorService;


        public AuthController(SignInManager<IdentityUser> signInManager,
                                UserManager<IdentityUser> userManager,
                                IOptions<JwtSettings> jwtsettings,
                                IVendedorService vendedorService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtsettings.Value;
            _vendedorService = vendedorService;

        }


        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            
            
            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };
            

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if(result.Succeeded)
            {
                // cria o Vendedor
                await _vendedorService.CriarAsync(new Vendedor
                {
                    VendedorId = user.Id,
                    VendedorEmail = user.Email,
                    VendedorNomeCompleto = registerUser.NomeCompleto
                });
                
                await _signInManager.SignInAsync(user,false);
                return Ok(await GerarJwt(user.Email));

            }

            return Problem("Falha ao regstrar o usuário!");              
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if(result.Succeeded)
            {
                return Ok(await GerarJwt(loginUser.Email));

            }

            return Problem("Usuário ou senha incorretos!");

        }

        private async Task<string> GerarJwt(string email)
        {
            
            var user = await _userManager.FindByEmailAsync(email);

             var claims = new List<Claim>
    
            {

                new Claim(JwtRegisteredClaimNames.Sub, user.Id),

                new Claim(ClaimTypes.NameIdentifier, user.Id),

            };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);         
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtSettings.Emissor,
                Audience = _jwtSettings.Audiencia,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(key),
                                         SecurityAlgorithms.HmacSha256Signature)
            });

            var encondedToken = tokenHandler.WriteToken(token);
            return encondedToken;
        }
    }
}