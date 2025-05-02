using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MoneyMate_WebApp.DataAccess.Entities;
using MoneyMate_WebApp.Models.UserAccount;

namespace MoneyMate_WebApp.Controllers;

public class UserAccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserAccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Register() => View(new RegisterViewModel());

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            Name = model.Name
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            foreach (var e in result.Errors)
                ModelState.AddModelError(string.Empty, e.Description);
            return View(model);
        }

        await _userManager.AddToRoleAsync(user, "User");
        await _signInManager.SignInAsync(user, isPersistent: false);
        return RedirectToAction("Index", "Account");
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _signInManager.PasswordSignInAsync(
            model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
            return LocalRedirect(model.ReturnUrl ?? Url.Action("Index", "Account")!);

        ModelState.AddModelError(string.Empty, "Невірний логін чи пароль");
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "UserAccount");
    }

    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View(new ChangePasswordViewModel());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToAction("Login");

        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(model);
        }

        await _signInManager.RefreshSignInAsync(user);
        TempData["StatusMessage"] = "Your password has been changed.";
        return RedirectToAction("Index", "Account");
    }
    [HttpGet]
    public async Task<IActionResult> EditProfile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToAction(nameof(Login));

        var model = new EditProfileViewModel
        {
            Email = user.Email,
            Name = user.Name
        };
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProfile(EditProfileViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToAction(nameof(Login));

        if (model.Email != user.Email)
        {
            var setEmail = await _userManager.SetEmailAsync(user, model.Email);
            if (!setEmail.Succeeded)
            {
                foreach (var e in setEmail.Errors)
                    ModelState.AddModelError(string.Empty, e.Description);
                return View(model);
            }
            user.UserName = model.Email;
        }

        user.Name = model.Name;

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            foreach (var e in updateResult.Errors)
                ModelState.AddModelError(string.Empty, e.Description);
            return View(model);
        }

        await _signInManager.RefreshSignInAsync(user);
        TempData["StatusMessage"] = "Profile updated successfully.";
        return RedirectToAction("Index", "Account");
    }
}
