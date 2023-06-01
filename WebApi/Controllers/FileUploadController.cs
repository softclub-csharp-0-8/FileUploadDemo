
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Services;

[ApiController]
[Route("[controller]")]
public class FileUploadController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly QuoteService _quoteService;

    public FileUploadController(IWebHostEnvironment webHostEnvironment, QuoteService quoteService)
    {
        _webHostEnvironment = webHostEnvironment;
        _quoteService = quoteService;
    }

    [HttpGet("GetList")]
    public List<GetQuoteDto> GetListOfFiles()
    {
        return _quoteService.GetQuotes();
    }

    [HttpPost("Add")]
    public GetQuoteDto UploadFile([FromForm] AddQuoteDto quote)
    {
        return _quoteService.AddQuote(quote);
    }

    
    [HttpPut("Update")]
    public GetQuoteDto Update([FromForm] AddQuoteDto quote)
    {
        return _quoteService.UpdateQuote(quote);
    }


}