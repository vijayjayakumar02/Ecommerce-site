using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomIdentity.Data.Entities;
using WebAPI.Models.BindingModel;

namespace WebAPI.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AdministrationController(RoleManager<AppRole> roleManager,UserManager<AppUser> userManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        
        [HttpGet("ListUsers")]
        public IEnumerable<AppUser> ListUsers()
        {
            List<AppUser> user = new List<AppUser>();
            var users = _userManager.Users;
            foreach(AppUser u in users)
            {
                user.Add(new AppUser
                {
                    UserName = u.UserName
                });
            }
            return user;
        }

        [HttpPost("CreateRole")]
        public async Task<Object> CreateRole([FromBody] CreateRoleBindingModel model)
        {
            try
            {
                var role = new AppRole
                {
                    Name = model.RoleName   
                };
                var res = await _roleManager.CreateAsync(role);
                if (res.Succeeded)
                {
                    return "role successfully added";
                }
                return string.Join(",", res.Errors.Select(x => x.Description).ToArray()); 
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        
    }
}
