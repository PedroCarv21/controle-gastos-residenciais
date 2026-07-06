using System.ComponentModel.DataAnnotations;

namespace ResidentialExpenseControl.Api.DTOs.Person;

public class PersonRequestDTO
{
    [Required(ErrorMessage = "O nome é necessário.")]
    [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "A idade é necessária.")]
    [Range(0, 120, ErrorMessage = "Idade deve estar entre 0 e 120.")]
    public int? Age { get; set; }
}