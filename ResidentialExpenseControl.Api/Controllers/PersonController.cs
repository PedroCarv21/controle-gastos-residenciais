using Microsoft.AspNetCore.Mvc;
using ResidentialExpenseControl.Api.DTOs.Person;
using ResidentialExpenseControl.Api.DTOs.Total;
using ResidentialExpenseControl.Api.Responses;
using ResidentialExpenseControl.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResidentialExpenseControl.Api.Controllers;

/// <summary>
/// The controller will be used to define the access rules for each method and the expected responses, 
/// such as the URL, status code, HTTP verb, etc. 
/// This API will be accessed via 'api/people'.
/// </summary>

[ApiController]
[Route("api/people")]
public class PersonController : ControllerBase
{
    private readonly PersonService _personService;

    /// <summary>
    /// Initializing the PersonService via the constructor
    /// </summary>
    /// <param name="personService">PersonService object</param>
    public PersonController(PersonService personService)
    {
        _personService = personService;
    }

    /// <summary>
    /// Capture the person's ID.
    /// </summary>
    /// <param name="id">Guid id</param>
    /// <returns>PersonResponseDTO object</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PersonResponseDTO>> GetById(Guid id)
    {
        var person = await _personService.GetByIdAsync(id);

        if (person is null)
        {
            return NotFound();
        }

        return Ok(person);
    }

    /// <summary>
    /// List all people and return a 200 status.
    /// </summary>
    /// <returns>List of PersonResponseDTO objects</returns>
    [HttpGet]
    public async Task<ActionResult<List<PersonResponseDTO>>> GetAll()
    {
        var people = await _personService.GetAllAsync();

        return Ok(people);
    }

    /// <summary>
    /// It receives the name and age information and returns this data along with the ID of the newly 
    /// registered person. The returned status code is 201.
    /// </summary>
    /// <param name="dto">PersonRequestDTO object</param>
    /// <returns>PersonResponseDTO object</returns>
    [HttpPost]
    public async Task<ActionResult<PersonResponseDTO>> Create(PersonRequestDTO dto)
    {
        try
        {
            var person = await _personService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = person.Id },
                person);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ErrorResponse
            {
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// It receives the ID of an already registered person and attempts to delete them from the database. 
    /// If the ID is not found, the returned status code will be 404, 
    /// but if the ID is found, the status code will be 204.
    /// </summary>
    /// <param name="id">Guid id</param>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _personService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Lists all registered individuals, showing the total income, expenses, and balance for each person. 
    /// The total income, total expenses, and net balance are also displayed at the end of the list.
    /// The returned status code is 200.
    /// </summary>
    /// <returns>TotalResponseDTO object</returns>
    [HttpGet("totals")]
    public async Task<ActionResult<TotalsResponseDTO>> GetTotals()
    {
        var totals = await _personService.GetTotalsAsync();

        return Ok(totals);
    }
}