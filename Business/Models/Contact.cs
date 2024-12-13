using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class Contact
{
    public string Id { get; set; } = null!;

    [Required (ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Not a valid email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Phone Number is required")]
    public string PhoneNumber { get; set; } = null!;

    [Required(ErrorMessage = "Street is required")]
    public string Street { get; set; } = null!;

    [Required(ErrorMessage = "ZipCode is required")]
    public string ZipCode { get; set; } = null!;

    [Required(ErrorMessage = "City is required")]
    public string City { get; set; } = null!;
}
