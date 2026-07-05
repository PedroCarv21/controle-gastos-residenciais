using ResidentialExpenseControl.Api.DTOs.Person;
using ResidentialExpenseControl.Api.Models;
using ResidentialExpenseControl.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResidentialExpenseControl.Api.Services;

public class PersonService
{
    private readonly PersonRepository _personRepository;

    public PersonService(PersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    private static PersonResponseDTO ToResponse(Person person)
    {
        return new PersonResponseDTO
        {
            Id = person.Id,
            Name = person.Name,
            Age = person.Age
        };
    }

    public async Task<List<PersonResponseDTO>> GetAllAsync()
    {
        var people = await _personRepository.GetAllAsync();

        return people.Select(ToResponse).ToList();
    }

    public async Task<PersonResponseDTO> CreateAsync(PersonRequestDTO dto)
    {

        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = dto.Name.Trim(),
            Age = dto.Age
        };

        await _personRepository.CreateAsync(person);

        return ToResponse(person);
    }

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
}