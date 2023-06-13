using Domain.Dtos;
using Domain.Dtos.Employee;
using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController :ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("get-employees")]
    public async Task<List<GetEmployeeDto>> GetEmployee()
    {
        return await _employeeService.GetEmployees();
    }
    
    [HttpPost("add-employee")]
    public async Task<AddEmployeeDto> AddEmployee(AddEmployeeDto model)
    {
        return await _employeeService.AddEmployee(model);
    }
    
    [HttpPut("update-employee")]
    public async Task<AddEmployeeDto> UpdateEmployee(AddEmployeeDto model)
    {
        return await _employeeService.UpdateEmployee(model);
    }
    
    [HttpDelete("delete-employee")]
    public async Task<bool> DeleteEmployee(int id)
    {
        return await _employeeService.DeleteEmployee(id);
    }
}