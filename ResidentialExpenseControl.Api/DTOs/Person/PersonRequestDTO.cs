using System.ComponentModel.DataAnnotations;

namespace ResidentialExpenseControl.Api.DTOs.Person;

public class PersonRequestDTO
{
    [Required(ErrorMessage = "Name is needed.")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(0, 120)]
    public int Age { get; set; }
}