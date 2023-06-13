using Domain.Dtos;
using Domain.Dtos.Employee;
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
    
    public async Task<List<GetEmployeeDto>> GetEmployees()
    {
        return await _context.Employees.Select(e=> new GetEmployeeDto()
        {
            Id = e.Id,
            FullName = e.FullName,
            CompanyId = e.CompanyId,
            CompanyName = e.Company.Name
        }).OrderBy(e=>e.Id).ToListAsync(); // select * from employees
    }

    public async Task<AddEmployeeDto> AddEmployee(AddEmployeeDto model)
    {
        var employee = new Employee(model.Id, model.FullName, model.CompanyId);
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
        model.Id = employee.Id;
        return model;
    }
    
    
    public async Task<AddEmployeeDto> UpdateEmployee(AddEmployeeDto model)
    {
        var existing = await _context.Employees.FindAsync(model.Id);
        existing.CompanyId = model.CompanyId;
        existing.FullName = model.FullName;
        await _context.SaveChangesAsync();
        return model;
    }
    
    public async Task<bool> DeleteEmployee(int id)
    {
        var existing = await _context.Employees.FindAsync(id);
        if (existing == null) return false;
        
        _context.Employees.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }

}