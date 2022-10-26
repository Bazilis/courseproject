using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserManagementService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> IsLogOutBlockUsers(string[] usersNames, string currentUserName)
        {
            bool isLogOut = false;
            foreach (var userName in usersNames)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user.IsBlocked)
                    continue;
                if (userName == currentUserName)
                    isLogOut = true;
                user.IsBlocked = true;
                await _userManager.UpdateAsync(user);
            }
            return isLogOut;
        }

        public async Task UnblockUsers(string[] usersNames)
        {
            foreach (var userName in usersNames)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (!user.IsBlocked)
                    continue;
                user.IsBlocked = false;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<bool> IsLogOutDeleteUsers(string[] usersNames, string currentUserName)
        {
            bool isLogOut = false;
            foreach (var userName in usersNames)
            {
                if (userName == currentUserName)
                    isLogOut = true;
                var user = await _userManager.FindByNameAsync(userName);
                await _userManager.DeleteAsync(user);
            }

            return isLogOut;
        }

        public async Task AddUsersToRoleAdmin(string[] usersNames)
        {
            foreach (var userName in usersNames)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                    continue;
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }

        public async Task AddUsersToRoleUser(string[] usersNames)
        {
            foreach (var userName in usersNames)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (await _userManager.IsInRoleAsync(user, "User"))
                    continue;
                await _userManager.AddToRoleAsync(user, "User");
            }
        }

        public async Task<bool> IsLogOutRemoveUsersFromRoleAdmin(string[] usersNames, string currentUserName)
        {
            bool isLogOut = false;
            foreach (var userName in usersNames)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (!await _userManager.IsInRoleAsync(user, "Admin"))
                    continue;

                if (userName == currentUserName)
                {
                    var userList = await _userManager.GetUsersInRoleAsync("Admin");

                    // if current user is only one in Admin role
                    // he can't delele himself from Admin role
                    // he must leave at least one other user in Admin role
                    if (!userList.Any(u => u.UserName != userName))
                        continue;

                    await _userManager.RemoveFromRoleAsync(user, "Admin");
                    isLogOut = true;
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, "Admin");
                }
            }

            return isLogOut;
        }

        public async Task RemoveUsersFromRoleUser(string[] usersNames)
        {
            foreach (var userName in usersNames)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (!await _userManager.IsInRoleAsync(user, "User"))
                    continue;

                await _userManager.RemoveFromRoleAsync(user, "User");
            }
        }
    }
}
