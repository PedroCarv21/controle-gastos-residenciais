using ResidentialExpenseControl.Api.DTOs.Transaction;
using ResidentialExpenseControl.Api.Enums;
using ResidentialExpenseControl.Api.Models;
using ResidentialExpenseControl.Api.Repositories;

namespace ResidentialExpenseControl.Api.Services;

public class TransactionService
{
    private readonly TransactionRepository _transactionRepository;
    private readonly PersonRepository _personRepository;

    public TransactionService(
        TransactionRepository transactionRepository,
        PersonRepository personRepository)
    {
        _transactionRepository = transactionRepository;
        _personRepository = personRepository;
    }

    public async Task<List<TransactionResponseDTO>> GetAllAsync()
    {
        var transactions = await _transactionRepository.GetAllAsync();

        return transactions.Select(ToResponse).ToList();
    }

    public async Task<TransactionResponseDTO> CreateAsync(TransactionRequestDTO dto)
    {
        var person = await _personRepository.GetByIdAsync(dto.PersonId);

        if (person is null)
        {
            throw new ArgumentException("ID incorreto.");
        }

        if (person.Age < 18 && dto.Type == TransactionType.Income)
        {
            throw new InvalidOperationException(
                "Pessoas menores de idade só podem possuir despesas.");
        }

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Description = dto.Description.Trim(),
            Amount = dto.Amount,
            Type = dto.Type,
            PersonId = person.Id,
            Person = person
        };

        await _transactionRepository.CreateAsync(transaction);

        return ToResponse(transaction);
    }

    private static TransactionResponseDTO ToResponse(Transaction transaction)
    {
        return new TransactionResponseDTO
        {
            Id = transaction.Id,
            Description = transaction.Description,
            Amount = transaction.Amount,
            Type = transaction.Type,
            PersonId = transaction.PersonId,
            PersonName = transaction.Person?.Name ?? string.Empty
        };
    }
}