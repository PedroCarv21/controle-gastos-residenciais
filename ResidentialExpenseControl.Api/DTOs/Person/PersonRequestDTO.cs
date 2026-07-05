using System.ComponentModel.DataAnnotations;

namespace ResidentialExpenseControl.Api.DTOs.Person;

public class PersonRequestDTO
{
    [Required(ErrorMessage = "The name is required.")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(0, 120)]
    public int Age { get; set; }
}