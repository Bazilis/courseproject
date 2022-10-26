using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class DbInitializerService : IDbInitializerService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public DbInitializerService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task Initialize()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new AppRole() { Name = "Admin" });
                await _roleManager.CreateAsync(new AppRole() { Name = "User" });

                await _userManager.CreateAsync(new AppUser
                {
                    UserName = "Bob",
                    Email = "bob@bob.com",
                    EmailConfirmed = true,
                }, "1");

                var user = await _userManager.FindByNameAsync("Bob");

                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
