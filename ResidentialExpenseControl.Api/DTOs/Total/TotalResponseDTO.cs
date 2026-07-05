namespace ResidentialExpenseControl.Api.DTOs.Total;

public class TotalsResponseDTO
{
    public List<PersonTotalDTO> People { get; set; } = [];

    public SummaryDTO Summary { get; set; } = new();
}