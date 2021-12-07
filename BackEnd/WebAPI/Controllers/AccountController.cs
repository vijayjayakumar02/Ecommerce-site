using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomIdentity.Data.Entities;
using WebAPI.Models.BindingModel;
using WebAPI.Models.DTO;
using WebAPI.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using WebAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JWTConfig _jWTConfig;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<JWTConfig> jwtConfig)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jWTConfig = jwtConfig.Value;
        }
        [HttpPost("RegisterUser")]
        public async Task<Object> RegisterUser([FromBody] RegisterUserbindingModel model)
        {
            try
            {
                var user = new AppUser()
                {
                    Email = model.Email,
                    UserName = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return "user successfully created";
                }
                return string.Join(",", result.Errors.Select(x => x.Description).ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpPost("Login")]
        public async Task<Object> Login([FromBody] LoginBindingModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        var getUser = await _userManager.FindByEmailAsync(model.Email);
                        var user = new UserDTO(getUser.Email, getUser.UserName);
                        user.Token = GenerateToken(getUser);

                        return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Logged Successfully", user));
                    }
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Invalid Login attempt", null));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, e.Message, null));
            }
        }
        private string GenerateToken(AppUser appUser)
        {
            var jwtTokenHolder = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(_jWTConfig.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[] {
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId, appUser.Id.ToString()),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jWTConfig.Audience,
                Issuer = _jWTConfig.Issuer
            };
            var token = jwtTokenHolder.CreateToken(tokenDescriptor);
            return jwtTokenHolder.WriteToken(token);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllUser")]
        public async Task<Object> GetAllUser()
        {
            try
            {
                var users = _userManager.Users.Select(x => new UserDTO( x.Email, x.UserName)).ToList();//using select is same as like using js map keyword
                return await Task.FromResult(users);
            }
            catch (Exception e)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, e.Message, null));
            }
        }
    }
}
