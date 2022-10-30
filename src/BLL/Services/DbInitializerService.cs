using BLL.Interfaces;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class DbInitializerService : IDbInitializerService
    {
        private readonly AppDbContext _dbContext;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public DbInitializerService(
            AppDbContext dbContext,
            RoleManager<AppRole> roleManager,
            UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task Initialize()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                    _dbContext.Database.Migrate();
            }
            catch (Exception)
            {
            }

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
