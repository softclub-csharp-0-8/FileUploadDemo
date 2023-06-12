using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class EmployeeService
{
    private readonly DataContext _context;

    public EmployeeService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<List<EmployeeDto>> GetEmployees()
    {
        return await _context.Employees.Select(e=> new EmployeeDto()
        {
            Id = e.Id,
            FullName = e.FullName,
            CompanyId = e.CompanyId,
            CompanyName = e.Company.Name
        }).ToListAsync(); // select * from employees
    }
    
    // public async Task<Quote> GetQuoteById(int id)
    // {
    //     return await _context.Quotes.FindAsync(id);
    // }
    //
    // public async Task<Quote> AddQuote(Quote quote)
    // {
    //     await _context.Quotes.AddAsync(quote); // insert into quotes
    //     await _context.SaveChangesAsync();
    //     return quote;
    // }
    
}