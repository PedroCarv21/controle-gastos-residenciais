using Microsoft.AspNetCore.Mvc;
using ResidentialExpenseControl.Api.DTOs.Transaction;
using ResidentialExpenseControl.Api.Services;

namespace ResidentialExpenseControl.Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionController : ControllerBase
{
    private readonly TransactionService _transactionService;

    public TransactionController(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TransactionResponseDTO>>> GetAll()
    {
        var transactions = await _transactionService.GetAllAsync();

        return Ok(transactions);
    }

    [HttpPost]
    public async Task<ActionResult<TransactionResponseDTO>> Create(TransactionRequestDTO dto)
    {
        try
        {
            var transaction = await _transactionService.CreateAsync(dto);

            return StatusCode(StatusCodes.Status201Created, transaction);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new
            {
                message = exception.Message
            });
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(new
            {
                message = exception.Message
            });
        }
    }
}