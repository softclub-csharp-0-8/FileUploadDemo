
using Domain.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<List<GetQuoteDto>> GetListOfFiles()
    {
        return await _quoteService.GetQuotes();
    }

    [HttpPost("Add")]
    [DisableRequestSizeLimit,
     RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue, 
         ValueLengthLimit = int.MaxValue)]
    public GetQuoteDto UploadFile([FromForm] AddQuoteDto quote)
    {
        return  _quoteService.AddQuote(quote);
    }
    
    [HttpGet("sync1")]
    public string Syncrounous1()
    {
        Thread.Sleep(15000);
        return "Hi";
    }
    
    [HttpGet("sync2")]
    public string Syncrounous2()
    {
        // Thread.Sleep(15000);
        return "Hi from sync 2";
    }

    
    [HttpPut("Update")]
    public async Task<GetQuoteDto> Update([FromForm] AddQuoteDto quote)
    {
        return  _quoteService.UpdateQuote(quote);
    }


}