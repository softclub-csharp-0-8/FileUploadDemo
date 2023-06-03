using Dapper;
using Domain.Dtos;
using Infrastructure.Context;

namespace Infrastructure.Services;

public class QuoteService
{
    private readonly DapperContext _context;
    private readonly IFileService _fileService;

    public QuoteService(DapperContext context,IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<List<GetQuoteDto>> GetQuotes()
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = "select id, author,quotetext,categoryid,file_name as filename from quotes order by id;";
            var result =  await conn.QueryAsync<GetQuoteDto>(sql);
            return result.ToList();
        }
    }
    
    public async Task<GetQuoteDto> GetQuoteById(int id)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = "select id, author,quotetext,categoryid,file_name as filename from quotes where id=@id";
            var result =  await conn.QuerySingleOrDefaultAsync<GetQuoteDto>(sql,new {id});
            return result;
        }
    }

    public GetQuoteDto AddQuote(AddQuoteDto quote)
    {
        using (var conn = _context.CreateConnection())
        {
            //upload file
            var filename =  _fileService.CreateFileAsync("images", quote.File);
            var sql = "insert into quotes (author, quotetext, categoryid, file_name) VALUES (@author, @quotetext, @categoryid, @filename) returning id;";
            var result =  conn.ExecuteScalar<int>(sql,new
            {
                quote.Author,
                quote.QuoteText,
                quote.CategoryId,
                filename
            });
            return new GetQuoteDto()
            {
                Author = quote.Author,
                QuoteText = quote.QuoteText,
                CategoryId = quote.CategoryId,
                FileName = filename,
                Id = result
            };
        }
    }

    public GetQuoteDto UpdateQuote(AddQuoteDto quote)
    {
        using (var conn = _context.CreateConnection())
        {
            var existing =
                conn.QuerySingleOrDefault<GetQuoteDto>(
                    "select id, author,quotetext,categoryid,file_name as filename from quotes where id=@id;",
                    new {quote.Id});
            if (existing == null)
            {
                return new GetQuoteDto();
            }

            string filename = null;
            // if file not found on database ->just add
            //if file found in database and file is found in quote -> delete file in database and add new file
            //if file found in database and not found in quote -> just do nothing
            if (quote.File != null && existing.FileName != null)
            {
                _fileService.DeleteFile("images", existing.FileName);
                filename =  _fileService.CreateFileAsync("images", quote.File);
            }
            else if (quote.File != null && existing.FileName == null)
            {
                filename =  _fileService.CreateFileAsync("images", quote.File);
            }
            var sql = "update quotes set author=@author, quotetext=@quotetext,categoryid=@categoryid  where id=@id";
            if (quote.File != null)
            {
                sql =
                    "update quotes set author=@author, quotetext=@quotetext,categoryid=@categoryid,file_name=@filename where id=@id";
            }
            var result =  conn.Execute(sql,new
            {
                quote.Author,
                quote.QuoteText,
                quote.CategoryId,
                filename,
                quote.Id
            });
            return new GetQuoteDto()
            {
                Author = quote.Author,
                QuoteText = quote.QuoteText,
                CategoryId = quote.CategoryId,
                FileName = filename,
                Id = result
            };
        }
    }
}