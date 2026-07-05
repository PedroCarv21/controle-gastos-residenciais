using System.ComponentModel.DataAnnotations;

namespace ResidentialExpenseControl.Api.Models;

public class Person
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(0, 120)]
    public int Age { get; set; }

    public List<Transaction> Transactions { get; set; } = [];
}