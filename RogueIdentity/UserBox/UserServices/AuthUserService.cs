using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RogueIdentity.ApiDbContext;
using RogueIdentity.Models;
using RogueIdentity.UserBox.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RogueIdentity.UserBox.UserServices
{
    public class AuthUserService : IAuthMyUser
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;

        public AuthUserService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            ApplicationDbContext db
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _db = db;
        }

        public AuthUserService()
        {
        }

        public async Task<object> LoginAsync(LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return await GenerateMyUserJwtToken(model.Email, appUser);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        public async Task<object> RegisterAsync(RegisterDto model)
        {
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return await GenerateMyUserJwtToken(model.Email, user);
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }


        public async Task<object> GenerateMyUserJwtToken(string email, IdentityUser user)
        {
          
            var claims = new List<Claim>
            {   
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                #region add more claims
                //new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                //new Claim(ClaimTypes.GivenName, user.NormalizedUserName)
                #endregion  
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Dont put here this is my custom Secret key for authnetication"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(30));

            var token = new JwtSecurityToken(
                _configuration["http://localhost:51237/"],
                _configuration["http://localhost:51237/"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            //object u = await Task.Run(() =>
            //{
                return new JwtSecurityTokenHandler().WriteToken(token);
            //});

            //return  u;
        }
    }
}
