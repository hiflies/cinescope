using Microsoft.AspNetCore.Mvc;

namespace CineScope.Models;

public class CreateAccountModel
{
    [FromForm(Name = "email")] public string Email { get; set; }
    [FromForm(Name = "username")] public string Username { get; set; }
    [FromForm(Name = "password")] public string Password { get; set; }
    [FromForm(Name = "confirm-password")] public string ConfirmPassword { get; set; }
}