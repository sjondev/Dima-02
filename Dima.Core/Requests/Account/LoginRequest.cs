using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Account;

public class LoginRequest : Request
{
    [Required(ErrorMessage = "E-mail is required")]
    [EmailAddress(ErrorMessage = "E-mail is not valid")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}