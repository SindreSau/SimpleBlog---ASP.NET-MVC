using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Models;

namespace SimpleBlog.Controllers
{
    public class AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        : Controller
    {
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await signInManager.PasswordSignInAsync(model.UsernameOrEmail, model.Password,
                    model.RememberMe, lockoutOnFailure: false);

                if (!result.Succeeded)
                {
                    // If the login fails, try to find the user by email
                    var user = await userManager.FindByEmailAsync(model.UsernameOrEmail);
                    if (user != null)
                    {
                        if (user.UserName != null)
                            result = await signInManager.PasswordSignInAsync(user.UserName, model.Password,
                                model.RememberMe, lockoutOnFailure: false);
                    }
                }

                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public IActionResult Register(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Username, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToLocal(returnUrl);
                }

                AddErrors(result);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(BlogPostController.Index), "BlogPost");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(BlogPostController.Index), "BlogPost");
            }
        }


        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var user = await userManager.GetUserAsync(User);
            switch (user)
            {
                case null:
                    return RedirectToAction(nameof(BlogPostController.Index), "BlogPost");
                case { UserName: not null, Email: not null }:
                {
                    var model = new ManageAccountViewModel
                    {
                        Username = user.UserName,
                        Email = user.Email
                    };

                    return View(model);
                }
                default:
                    return RedirectToAction(nameof(BlogPostController.Index), "BlogPost");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Manage(ManageAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction(nameof(BlogPostController.Index), "BlogPost");
            }

            if (model.Username != user.UserName)
            {
                var setUsernameResult = await userManager.SetUserNameAsync(user, model.Username);
                if (!setUsernameResult.Succeeded)
                {
                    foreach (var error in setUsernameResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
            }

            await signInManager.RefreshSignInAsync(user);
            TempData["StatusMessage"] = "Your username has been updated";
            return RedirectToAction(nameof(Manage));
        }
    }
}