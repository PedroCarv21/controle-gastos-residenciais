using Microsoft.AspNetCore.Mvc;
using ResidentialExpenseControl.Api.DTOs.Transaction;
using ResidentialExpenseControl.Api.Services;

namespace ResidentialExpenseControl.Api.Controllers;
/// <summary>
/// The controller will be used to define the access rules for each method and the expected responses, 
/// such as the URL, status code, HTTP verb, etc. 
/// This API will be accessed via 'api/transactions'.
/// </summary>
[ApiController]
[Route("api/transactions")]
public class TransactionController : ControllerBase
{
    private readonly TransactionService _transactionService;

    /// <summary>
    /// Initializing the TransactionService via the constructor
    /// </summary>
    /// <param name="transactionService">TransactionService object</param>
    public TransactionController(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    /// <summary>
    /// List all transactions and return a 200 status.
    /// </summary>
    /// <returns>List of TransactionResponseDTO objects</returns>
    [HttpGet]
    public async Task<ActionResult<List<TransactionResponseDTO>>> GetAll()
    {
        var transactions = await _transactionService.GetAllAsync();

        return Ok(transactions);
    }

    /// <summary>
    /// It receives the description, amount, person ID, and transaction type, 
    /// saves them to the database, and returns a 201 status code.
    /// </summary>
    /// <param name="dto">TransactionRequestDTO object</param>
    /// <returns>TransactionResponseDTO object</returns>
    [HttpPost]
    public async Task<ActionResult<TransactionResponseDTO>> Create(TransactionRequestDTO dto)
    {
        try
        {
            var transaction = await _transactionService.CreateAsync(dto);

            return StatusCode(StatusCodes.Status201Created, transaction);
        }
        // If the provided person ID does not belong to any registered person,
        // an ArgumentException exception will occur.
        catch (ArgumentException exception)
        {
            return BadRequest(new
            {
                message = exception.Message
            });
        }
        // If the person is under 18 years of age and the transaction type is income,
        // an InvalidOperationException exception will occur.
        catch (InvalidOperationException exception)
        {
            return BadRequest(new
            {
                message = exception.Message
            });
        }
    }
}