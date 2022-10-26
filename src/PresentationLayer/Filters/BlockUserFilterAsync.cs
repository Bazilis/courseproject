using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PresentationLayer.Filters
{
    public class BlockUserFilterAsync : IAsyncResourceFilter
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public BlockUserFilterAsync(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var currentUser = await _userManager.GetUserAsync(context.HttpContext.User);

            if (currentUser != null && currentUser.IsBlocked)
            {
                context.ModelState.AddModelError(string.Empty, $"Unfortunately {currentUser.UserName} is blocked!");
                await _signInManager.SignOutAsync();
                context.Result = new RedirectResult("~/Home/Index");
            }

            else
                await next();
        }
    }
}
