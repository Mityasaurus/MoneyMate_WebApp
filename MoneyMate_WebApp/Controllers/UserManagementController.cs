using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyMate_WebApp.DataAccess.Entities;
using MoneyMate_WebApp.Models.UserManagement;

namespace MoneyMate_WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class UserManagementController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public UserManagementController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var userRoles = await _userManager.GetRolesAsync(user);
        var allRoles = _roleManager.Roles.Select(r =>
            new SelectListItem(r.Name, r.Name)).ToList();

        var vm = new EditUserViewModel
        {
            Id = user.Id.ToString(),
            Email = user.Email!,
            Name = user.Name,
            SelectedRoles = userRoles.ToList(),
            AvailableRoles = allRoles
        };
        return View(vm);
    }

    // POST: /UserManagement/Edit
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditUserViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm.AvailableRoles = _roleManager.Roles
                .Select(r => new SelectListItem(r.Name, r.Name))
                .ToList();
            return View(vm);
        }

        var user = await _userManager.FindByIdAsync(vm.Id);
        if (user == null) return NotFound();

        user.Email = vm.Email;
        user.UserName = vm.Email;
        user.Name = vm.Name;

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            foreach (var e in updateResult.Errors)
                ModelState.AddModelError(string.Empty, e.Description);
            vm.AvailableRoles = _roleManager.Roles
                .Select(r => new SelectListItem(r.Name, r.Name))
                .ToList();
            return View(vm);
        }

        var currentRoles = await _userManager.GetRolesAsync(user);
        var toAdd = vm.SelectedRoles.Except(currentRoles);
        var toRemove = currentRoles.Except(vm.SelectedRoles);

        if (toAdd.Any())
            await _userManager.AddToRolesAsync(user, toAdd);
        if (toRemove.Any())
            await _userManager.RemoveFromRolesAsync(user, toRemove);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Index()
    {
        var users = _userManager.Users.ToList();
        return View(users);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id))
            return BadRequest();

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        return View(user);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (string.IsNullOrEmpty(id))
            return BadRequest();

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            foreach (var e in result.Errors)
                ModelState.AddModelError(string.Empty, e.Description);
            return View("Delete", user);
        }

        return RedirectToAction(nameof(Index));
    }
}
