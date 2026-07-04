using System;

namespace ResidentialExpenseControl.Api.DTOs.Person;

public class PersonResponseDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }
}