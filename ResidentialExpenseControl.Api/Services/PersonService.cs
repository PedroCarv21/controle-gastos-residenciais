using ResidentialExpenseControl.Api.DTOs.Person;
using ResidentialExpenseControl.Api.Models;
using ResidentialExpenseControl.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResidentialExpenseControl.Api.DTOs.Total;
using ResidentialExpenseControl.Api.Enums;

namespace ResidentialExpenseControl.Api.Services;

/// <summary>
/// This class establishes the business rules for each of the methods 
/// that will be used to make requests in the table containing the person records.
/// </summary>
public class PersonService
{
    private readonly PersonRepository _personRepository;

    /// <summary>
    /// Initializing the PersonRepository via the constructor
    /// </summary>
    /// <param name="personRepository">PersonRepository object</param>
    public PersonService(PersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    /// <summary>
    /// Transfers data from a Person object to a PersonResponseDTO object.
    /// </summary>
    /// <param name="person">Person object</param>
    /// <returns>PersonRespondeDTO object</returns>
    private static PersonResponseDTO ToResponse(Person person)
    {
        return new PersonResponseDTO
        {
            Id = person.Id,
            Name = person.Name,
            Age = person.Age
        };
    }

    public async Task<PersonResponseDTO?> GetByIdAsync(Guid id)
    {
        var person = await _personRepository.GetByIdAsync(id);

        if (person is null)
        {
            return null;
        }

        return ToResponse(person);
    }

    /// <summary>
    /// Lists all people registered in the system.
    /// </summary>
    /// <returns>List of objects of type PersonResponseDTO.</returns>
    public async Task<List<PersonResponseDTO>> GetAllAsync()
    {
        var people = await _personRepository.GetAllAsync();

        return people.Select(ToResponse).ToList();
    }

    /// <summary>
    /// It receives a person's data, automatically generates an ID, 
    /// and saves this information to the database.
    /// </summary>
    /// <param name="dto">PersonRequestDTO object</param>
    /// <returns>PersonResponseDTO object</returns>
    public async Task<PersonResponseDTO> CreateAsync(PersonRequestDTO dto)
    {

        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = dto.Name.Trim(),
            Age = dto.Age!.Value
        };

        var exists = await _personRepository.ExistsByNameAsync(dto.Name);

        if (exists)
        {
            throw new InvalidOperationException("Já existe uma pessoa com esse nome.");
        }

        await _personRepository.CreateAsync(person);

        return ToResponse(person);
    }

    /// <summary>
    /// It receives a person's ID, and if that ID actually corresponds to a database record, 
    /// the person is deleted.
    /// </summary>
    /// <param name="id">Person ID</param>
    /// <returns>Returns true if the person was deleted and 
    /// returns false if the person's ID was not found.</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var person = await _personRepository.GetByIdAsync(id);

        if (person is null)
        {
            return false;
        }

        await _personRepository.DeleteAsync(person);

        return true;
    }

    /// <summary>
    /// Lists all registered individuals, showing the total income, expenses, and balance for each person. 
    /// The total income, total expenses, and net balance are also displayed at the end of the list.
    /// </summary>
    /// <returns>TotalResponseDTO object</returns>
    public async Task<TotalsResponseDTO> GetTotalsAsync()
    {
        var people = await _personRepository.GetAllAsync();

        var response = new TotalsResponseDTO();

        foreach (var person in people)
        {
            var totalIncome = person.Transactions
                .Where(transaction => transaction.Type == TransactionType.Income)
                .Sum(transaction => transaction.Value);

            var totalExpense = person.Transactions
                .Where(transaction => transaction.Type == TransactionType.Expense)
                .Sum(transaction => transaction.Value);

            response.People.Add(new PersonTotalDTO
            {
                PersonId = person.Id,
                Name = person.Name,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Balance = totalIncome - totalExpense
            });
        }

        response.Summary.TotalIncome = response.People.Sum(person => person.TotalIncome);
        response.Summary.TotalExpense = response.People.Sum(person => person.TotalExpense);
        response.Summary.Balance = response.Summary.TotalIncome - response.Summary.TotalExpense;

        return response;
    }
}