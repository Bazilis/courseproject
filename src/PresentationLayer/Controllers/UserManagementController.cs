using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(
            SignInManager<AppUser> signInManager,
            IUserManagementService userManagementService)
        {
            _signInManager = signInManager;
            _userManagementService = userManagementService;
        }

        // GET: UserManagementController
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BlockUsers(string[] usersNames)
        {
            if (await _userManagementService.IsLogOutBlockUsers(usersNames, User.Identity?.Name))
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UnblockUsers(string[] usersNames)
        {
            await _userManagementService.UnblockUsers(usersNames);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUsers(string[] usersNames)
        {
            if (await _userManagementService.IsLogOutDeleteUsers(usersNames, User.Identity?.Name))
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddUsersToRoleAdmin(string[] usersNames)
        {
            await _userManagementService.AddUsersToRoleAdmin(usersNames);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddUsersToRoleUser(string[] usersNames)
        {
            await _userManagementService.AddUsersToRoleUser(usersNames);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUsersFromRoleAdmin(string[] usersNames)
        {
            if(await _userManagementService.IsLogOutRemoveUsersFromRoleAdmin(usersNames, User.Identity?.Name))
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUsersFromRoleUser(string[] usersNames)
        {
            await _userManagementService.RemoveUsersFromRoleUser(usersNames);
            return RedirectToAction(nameof(Index));
        }
    }
}
