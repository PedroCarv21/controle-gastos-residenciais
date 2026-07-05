namespace ResidentialExpenseControl.Api.DTOs.Total;

public class SummaryDTO
{
    public decimal TotalIncome { get; set; }

    public decimal TotalExpense { get; set; }

    public decimal Balance { get; set; }
}