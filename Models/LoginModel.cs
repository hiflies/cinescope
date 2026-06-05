using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CineScope.Models;

public class LoginModel
{
    [Required]
    [FromForm(Name = "identifier")] public string Identifier { get; set; }
    [Required]
    [FromForm(Name = "password")] public string Password { get; set; }
    [FromForm(Name = "remember-me")] public bool RememberMe { get; set; }
}
