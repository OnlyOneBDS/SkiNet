using System.ComponentModel.DataAnnotations;

namespace SkiNet.Svc.DTOs;

public class RegisterDto
{
  [Required(ErrorMessage = "Name is required")]
  public string DisplayName { get; set; }

  [Required(ErrorMessage = "Email address is required")]
  [EmailAddress]
  public string Email { get; set; }

  [Required(ErrorMessage = "Password is required")]
  [
    RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
    ErrorMessage = "Password must have 1 uppcase, 1 lowercase, 1 number, 1 non alphnumeric, and at least 6 characters")
  ]
  public string Password { get; set; }
}