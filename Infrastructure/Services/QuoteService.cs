using Dapper;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class QuoteService
{
    private readonly DataContext _context;

    public QuoteService(DataContext context)
    {
        _context = context;
       
    }

    public async Task<List<Quote>> GetQuotes()
    {
        return await _context.Quotes.ToListAsync(); // select * from quotes
    }
    
    public async Task<Quote> GetQuoteById(int id)
    {
        return await _context.Quotes.FindAsync(id);
    }

    public async Task<Quote> AddQuote(Quote quote)
    {
        await _context.Quotes.AddAsync(quote); // insert into quotes
       await _context.SaveChangesAsync();
       return quote;
    }

    public async Task<Quote> UpdateQuote(Quote quote)
    {
        var find = await _context.Quotes.FindAsync(quote.Id);
        find.Author = quote.Author;
        find.QuoteText = quote.QuoteText;
        await _context.SaveChangesAsync();
        return quote;
    }

    public async Task<bool> Delete(int id)
    {
        var find = await _context.Quotes.FindAsync(id);
        _context.Quotes.Remove(find);
        await _context.SaveChangesAsync();
        return true;
    }
}