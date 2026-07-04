using System.ComponentModel.DataAnnotations;
using ResidentialExpenseControl.Api.Enums;

namespace ResidentialExpenseControl.Api.Models;

public class Transaction
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    public TransactionType Type { get; set; }

    public Guid PersonId { get; set; }

    public Person? Person { get; set; }
}