using ResidentialExpenseControl.Api.Enums;

namespace ResidentialExpenseControl.Api.DTOs.Transaction;

public class TransactionResponseDTO
{
    public Guid Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public decimal Value { get; set; }

    public TransactionType Type { get; set; }

    public Guid PersonId { get; set; } 
    public string PersonName { get; set; }
}