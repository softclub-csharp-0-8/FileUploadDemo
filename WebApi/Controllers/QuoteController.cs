using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class QuoteController :ControllerBase
{
    private readonly QuoteService _quoteService;

    public QuoteController(QuoteService quoteService)
    {
        _quoteService = quoteService;
    }

    [HttpGet("getquotes")]
    public async Task<List<Quote>> GetQuotes()
    {
        return await _quoteService.GetQuotes();
    }
    
    [HttpGet("GetquoteById")]
    public async Task<Quote> GetQuoteById(int id)
    {
        return await _quoteService.GetQuoteById(id);
    }
    
    [HttpPost("AddQuote")]
    public async Task<Quote> AddQuote(Quote quote)
    {
        return await _quoteService.AddQuote(quote);
    }

    
    [HttpPut("UpdateQuote")]
    public async Task<Quote> UpdateQuote(Quote quote)
    {
        return await _quoteService.UpdateQuote(quote);
    }
    
    [HttpDelete("DeleteQuote")]
    public async Task<bool> DeleteQuote(int id)
    {
        return await _quoteService.Delete(id);
    }

}