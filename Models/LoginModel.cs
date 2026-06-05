using Microsoft.AspNetCore.Mvc;

namespace CineScope.Models;

public class LoginModel
{
    [FromForm(Name = "identifier")] public string Identifier { get; set; }
    [FromForm(Name = "password")] public string Password { get; set; }
}
