using Microsoft.AspNetCore.Mvc;
using ResidentialExpenseControl.Api.DTOs.Person;
using ResidentialExpenseControl.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResidentialExpenseControl.Api.Controllers;

[ApiController]
[Route("api/people")]
public class PersonController : ControllerBase
{
    private readonly PersonService _personService;

    public PersonController(PersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PersonResponseDTO>>> GetAll()
    {
        var people = await _personService.GetAllAsync();

        return Ok(people);
    }

    [HttpPost]
    public async Task<ActionResult<PersonResponseDTO>> Create(PersonRequestDTO dto)
    {
        var person = await _personService.CreateAsync(dto);

        return StatusCode(StatusCodes.Status201Created, person);
    }

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
}