using CineScope.Entities;
using CineScope.Models;
using CineScope.Services;
using CineScope.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CineScope.Controllers;

public class AccountController(UserRepository repository) : Controller
{
    [HttpGet("/login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("/login")]
    public IActionResult LoginPost(LoginModel loginModel)
    {
        var user = repository.GetByIdentifier(loginModel.Identifier);
        if (user == null)
        {
            return BadRequest();
        }

        if (!CryptoUtils.VerifyHashedPassword(loginModel.Password, user.Password))
        {
            return BadRequest();
        }

        if (!user.IsActive)
        {
            return BadRequest();
        }
        
        return Redirect("/login?success=true");
    }

    [HttpGet("/create-account")]
    public IActionResult CreateAccount()
    {
        return View();
    }

    [HttpPost("/create-account")]
    public IActionResult CreateAccountPost(CreateAccountModel createAccountModel)
    {
        if (createAccountModel.Password != createAccountModel.ConfirmPassword)
        {
            return BadRequest();
        }

        repository.Add(new User
        {
            Email = createAccountModel.Email,
            Password = CryptoUtils.HashPassword(createAccountModel.Password),
            Username = createAccountModel.Username,
            Role = "user",
            IsActive = true
        });

        repository.Save();

        return RedirectToAction("Index", "Home");
    }
}