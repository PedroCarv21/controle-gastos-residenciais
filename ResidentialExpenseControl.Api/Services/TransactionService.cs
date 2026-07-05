using ResidentialExpenseControl.Api.DTOs.Transaction;
using ResidentialExpenseControl.Api.Enums;
using ResidentialExpenseControl.Api.Models;
using ResidentialExpenseControl.Api.Repositories;

namespace ResidentialExpenseControl.Api.Services;

/// <summary>
/// This class establishes the business rules for each of the methods 
/// that will be used to make requests in the table containing the transaction records.
/// </summary>
public class TransactionService
{
    private readonly TransactionRepository _transactionRepository;
    private readonly PersonRepository _personRepository;

    /// <summary>
    ///  Initializing the TransactionRepository and the PersonRepository
    ///  via the constructor.
    /// </summary>
    /// <param name="transactionRepository">TransactionRepository object</param>
    /// <param name="personRepository">PersonRepository object</param>
    public TransactionService(
        TransactionRepository transactionRepository,
        PersonRepository personRepository)
    {
        _transactionRepository = transactionRepository;
        _personRepository = personRepository;
    }

    /// <summary>
    /// Lists all transactions registered in the system.
    /// </summary>
    /// <returns>List of objects of type TransactionResponseDTO.</returns>
    public async Task<List<TransactionResponseDTO>> GetAllAsync()
    {
        var transactions = await _transactionRepository.GetAllAsync();

        return transactions.Select(ToResponse).ToList();
    }

    /// <summary>
    /// It receives a transaction's data, automatically generates an ID, 
    /// and saves this information to the database.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<TransactionResponseDTO> CreateAsync(TransactionRequestDTO dto)
    {
        var person = await _personRepository.GetByIdAsync(dto.PersonId);
        // If the ID does not belong to any registered person,
        // the result will be the message 'ID incorreto'.
        if (person is null)
        {
            throw new ArgumentException("ID incorreto.");
        }

        //If the person is under 18 years of age and the transaction type is income,
        //the result will be the error message:
        //'Pessoas menores de idade só podem possuir despesas.'
        if (person.Age < 18 && dto.Type == TransactionType.Income)
        {
            throw new InvalidOperationException(
                "Pessoas menores de idade só podem possuir despesas.");
        }

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Description = dto.Description.Trim(),
            Value = dto.Value,
            Type = dto.Type,
            PersonId = person.Id,
            Person = person
        };

        await _transactionRepository.CreateAsync(transaction);

        return ToResponse(transaction);
    }

    /// <summary>
    /// Transfers data from a Transaction object to a TransactionResponseDTO object.
    /// </summary>
    /// <param name="transaction">Transaction object</param>
    /// <returns>TransactionResponseDTO object</returns>
    private static TransactionResponseDTO ToResponse(Transaction transaction)
    {
        return new TransactionResponseDTO
        {
            Id = transaction.Id,
            Description = transaction.Description,
            Value = transaction.Value,
            Type = transaction.Type,
            PersonId = transaction.PersonId,
            PersonName = transaction.Person?.Name ?? string.Empty
        };
    }
}