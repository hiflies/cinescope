using System.Security.Claims;
using CineScope.Entities;
using CineScope.Models;
using CineScope.Services;
using CineScope.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace CineScope.Controllers;

public class AccountController(UserRepository repository) : Controller
{
    [HttpGet("/login")]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(new LoginModel());
    }

    [HttpPost("/login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginPost(LoginModel loginModel)
    {
        if (!ModelState.IsValid)
        {
            return View("Login", loginModel);
        }

        var user = repository.GetByIdentifier(loginModel.Identifier);
        if (user == null || !CryptoUtils.VerifyHashedPassword(loginModel.Password, user.Password))
        {
            ModelState.AddModelError(string.Empty, "Invalid email/username or password.");
            return View("Login", loginModel);
        }

        if (!user.IsActive)
        {
            ModelState.AddModelError(string.Empty, "This account has been deactivated.");
            return View("Login", loginModel);
        }

        await SignInUser(user, loginModel.RememberMe);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("/create-account")]
    public IActionResult CreateAccount()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(new CreateAccountModel());
    }

    [HttpPost("/create-account")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAccountPost(CreateAccountModel createAccountModel)
    {
        if (!ModelState.IsValid)
        {
            return View("CreateAccount", createAccountModel);
        }

        if (createAccountModel.Password != createAccountModel.ConfirmPassword)
        {
            ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
            return View("CreateAccount", createAccountModel);
        }

        if (repository.GetByIdentifier(createAccountModel.Email) != null ||
            repository.GetByIdentifier(createAccountModel.Username) != null)
        {
            ModelState.AddModelError(string.Empty, "An account with this email or username already exists.");
            return View("CreateAccount", createAccountModel);
        }

        var user = new User
        {
            Email = createAccountModel.Email,
            Password = CryptoUtils.HashPassword(createAccountModel.Password),
            Username = createAccountModel.Username,
            Role = "user",
            IsActive = true
        };

        repository.Add(user);
        repository.Save();

        await SignInUser(user, false);
        return RedirectToAction("Index", "Home");
    }

    [HttpPost("/logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    private async Task SignInUser(User user, bool isPersistent)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties { IsPersistent = isPersistent });
    }
}