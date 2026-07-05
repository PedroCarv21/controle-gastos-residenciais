using Microsoft.EntityFrameworkCore;
using ResidentialExpenseControl.Api.Data;
using ResidentialExpenseControl.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResidentialExpenseControl.Api.Repositories;

public class PersonRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context)
    {
        _context = context;
    }

   
    public async Task<List<Person>> GetAllAsync()
    {
        return await _context.People
            .Include(person => person.Transactions)
            .AsNoTracking()
            .OrderBy(person => person.Name)
            .ToListAsync();
    }

    public async Task<Person?> GetByIdAsync(Guid id)
    {
        return await _context.People.FindAsync(id);
    }

   
    public async Task<Person> CreateAsync(Person person)
    {
        _context.People.Add(person);

        await _context.SaveChangesAsync();

        return person;
    }

   
    public async Task DeleteAsync(Person person)
    {
        _context.People.Remove(person);

        await _context.SaveChangesAsync();
    }
}