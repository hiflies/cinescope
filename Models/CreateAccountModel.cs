using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CineScope.Models;

public class CreateAccountModel
{
    [Required]
    [EmailAddress]
    [FromForm(Name = "email")]
    public string Email { get; set; }

    [Required]
    [FromForm(Name = "username")]
    public string Username { get; set; }

    [Required]
    [FromForm(Name = "password")]
    public string Password { get; set; }

    [Required]
    [FromForm(Name = "confirm-password")]
    public string ConfirmPassword { get; set; }
}