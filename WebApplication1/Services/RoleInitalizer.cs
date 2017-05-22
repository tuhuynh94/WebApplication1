using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class RoleInitalizer
    {
        public static async Task Initialize(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> UserManager)
        {
            if(!await roleManager.RoleExistsAsync("Admin"))
            {
                var role = new IdentityRole("Admin");
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("User"))
            {
                var role = new IdentityRole("User");
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("Silver User"))
            {
                var role = new IdentityRole("Silver User");
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("Gold User"))
            {
                var role = new IdentityRole("Gold User");
                await roleManager.CreateAsync(role);
            }
        }
    }
}
