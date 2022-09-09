using System.ComponentModel.DataAnnotations;

namespace SkiNet.Svc.DTOs;

public class AddressDto
{
  [Required(ErrorMessage = "First Name is required")]
  public string FirstName { get; set; }

  [Required(ErrorMessage = "Last Name is required")]
  public string LastName { get; set; }

  [Required(ErrorMessage = "Street is required")]
  public string Street { get; set; }

  [Required(ErrorMessage = "City is required")]
  public string City { get; set; }

  [Required(ErrorMessage = "State is required")]
  public string State { get; set; }

  [Required(ErrorMessage = "Zipcode is required")]
  public string ZipCode { get; set; }
}