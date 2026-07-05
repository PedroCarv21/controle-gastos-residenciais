using System.ComponentModel.DataAnnotations;
using ResidentialExpenseControl.Api.Enums;

namespace ResidentialExpenseControl.Api.DTOs.Transaction;

public class TransactionRequestDTO
{
    [Required]
    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    public TransactionType Type { get; set; }

    [Required]
    public Guid PersonId { get; set; }
}