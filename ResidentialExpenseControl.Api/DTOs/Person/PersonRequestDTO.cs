using System.ComponentModel.DataAnnotations;

namespace ResidentialExpenseControl.Api.DTOs.Person;

public class PersonRequestDTO
{
    [Required(ErrorMessage = "Name is necessary.")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Age is necessary.")]
    [Range(0, 120, ErrorMessage = "Age must be between 0 and 120.")]
    public int? Age { get; set; }
}