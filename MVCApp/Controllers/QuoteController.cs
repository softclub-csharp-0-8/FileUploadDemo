using Domain.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVCApp.Controllers;

public class QuoteController : Controller
{
    private readonly QuoteService _quoteService;

    public QuoteController(QuoteService quoteService)
    {
        _quoteService = quoteService;
    }

    public async Task<IActionResult> Index()
    {
        var quotes = await _quoteService.GetQuotes();
        return View(quotes);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View(new AddQuoteDto());
    }
    
    [HttpPost]
    public IActionResult Create(AddQuoteDto quote)
    {
        if (ModelState.IsValid)
        {
            _quoteService.AddQuote(quote);
            return RedirectToAction("Index");
        }
        return View(quote);
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var existing = await _quoteService.GetQuoteById(id);
        var addquote = new AddQuoteDto()
        {
            Id = existing.Id,
            Author = existing.Author,
            CategoryId = existing.CategoryId,
            QuoteText = existing.QuoteText
        };
        return View(addquote);
    }
    
    [HttpPost]
    public IActionResult Update(AddQuoteDto quote)
    {
        if (ModelState.IsValid)
        {
            _quoteService.UpdateQuote(quote);
            return RedirectToAction("Index");
        }
        return View(quote);
    }
    
}