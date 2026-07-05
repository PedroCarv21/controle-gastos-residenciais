namespace ResidentialExpenseControl.Api.DTOs.Total;

public class PersonTotalDTO
{
    public Guid PersonId { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal TotalIncome { get; set; }

    public decimal TotalExpense { get; set; }

    public decimal Balance { get; set; }
}