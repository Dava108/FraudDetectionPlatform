using Microsoft.AspNetCore.Mvc;
using TransactionApi.Models;
using TransactionApi.Services;
using TransactionApi.Dtos;

namespace TransactionApi.Controllers;

[ApiController]
[Route("transactions")]
public class TransactionsController(ITransactionService transactionService) : ControllerBase
{
    [HttpPost]
public async Task<IActionResult> Create(TransactionRequest request)
{
    var transaction = new Transaction
    {
        Amount = request.Amount,
        Currency = request.Currency,
        CardId = request.CardId,
        MerchantId = request.MerchantId
    };

    var createdTransaction = await transactionService.CreateAsync(transaction);

    return Created(
        $"/transactions/{createdTransaction.Id}",
        createdTransaction
    );
}

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var transactions = await transactionService.GetAllAsync();

        return Ok(transactions);
    }

    [HttpGet("{id:guid}")]
public async Task<IActionResult> GetById(Guid id)
{
    var transaction = await transactionService.GetByIdAsync(id);

    if (transaction is null)
    {
        return NotFound();
    }

    return Ok(transaction);
    }
}