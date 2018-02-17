using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RogueIdentity.ApiDbContext;
using RogueIdentity.Models;
using RogueIdentity.UserBox.UserServices;

namespace RogueIdentity.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
    
        //private readonly SignInManager<IdentityUser> _signInManager;
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly IConfiguration _configuration;
        //private readonly ApplicationDbContext _db;

        //public AccountController(
        //    UserManager<IdentityUser> userManager,
        //    SignInManager<IdentityUser> signInManager,
        //    IConfiguration configuration,
        //    ApplicationDbContext db
        //    )
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _configuration = configuration;
        //    _db = db;
        //}

        ////----------------Test Register-------------------///

        //[HttpGet("make")]
        //public async Task<IActionResult> MakeUser()
        //{
        //    RegisterDto model = new RegisterDto();
        //    model.Email = "Mo2@mail.com";
        //    model.Password = "Au_123456@1!";

        //    var user = new IdentityUser
        //    {
        //        UserName = model.Email,
        //        Email = model.Email,

        //        //tests
        //        PhoneNumber = "14165556667",
        //        NormalizedUserName = "Moe",
                
                
                
        //    };
        //    var result = await _userManager.CreateAsync(user, model.Password);

        //    if (result.Succeeded)
        //    {
        //        await _signInManager.SignInAsync(user, false);
        //        //add claims test
        //        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, model.Email));
        //        //await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, "Moe"));
        //        //await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.MobilePhone,user.PhoneNumber));


        //        return Ok(await GenerateJwtToken(model.Email, user));
        //    }

        //    throw new ApplicationException("UNKNOWN_ERROR");


       
        //}
        //login test:
        ////get claims
        //var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

        //if (result.Succeeded)
        //{
        //    var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
        //    return Ok( await GenerateJwtToken(model.Email, appUser));
        //}

        //throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        //----------------------------------------------//


        //  [HttpPost]
        [HttpPost("login")]
        public async Task<object> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var logMeIn = await new AuthUserService().LoginAsync(model);
            return logMeIn;
            #region refactor 
            //var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            //if (result.Succeeded)
            //{
            //    var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
            //    return await GenerateJwtToken(model.Email, appUser);
            //}

            //throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
            #endregion
        }

        // [HttpPost]
        [HttpPost("register")]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            var RegisterMe = await new AuthUserService().RegisterAsync(model);
            return RegisterMe;

            #region refactor 
            //var user = new IdentityUser
            //{
            //    UserName = model.Email,
            //    Email = model.Email
            //};
            //var result = await _userManager.CreateAsync(user, model.Password);

            //if (result.Succeeded)
            //{
            //    await _signInManager.SignInAsync(user, false);
            //    return await GenerateJwtToken(model.Email, user);
            //}
            #endregion



        }




       
  
    }
}

//prod use config 
////--------------testing Dev // 
//                ValidIssuer = " http://localhost:51237/",
//                ValidAudience = " http://localhost:51237/",
//                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Dont put here")),
//private async Task<object> GenerateJwtToken(string email, IdentityUser user)
//{
//    var claims = new List<Claim>
//            {
//                new Claim(JwtRegisteredClaimNames.Sub, email),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                new Claim(ClaimTypes.NameIdentifier, user.Id)
//            };

//    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
//    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//    var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

//    var token = new JwtSecurityToken(
//        _configuration["JwtIssuer"],
//        _configuration["JwtIssuer"],
//        claims,
//        expires: expires,
//        signingCredentials: creds
//    );

//    return new JwtSecurityTokenHandler().WriteToken(token);
//}
//    }