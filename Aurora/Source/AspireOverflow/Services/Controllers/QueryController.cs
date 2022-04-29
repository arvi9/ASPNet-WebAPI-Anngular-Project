using System.Net;
using Microsoft.AspNetCore.Mvc;
using AspireOverflow.Models;
using AspireOverflow.Services;

namespace AspireOverflow.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class QueryController : ControllerBase
{

    internal static ILogger<QueryController> _logger;
    private static QueryService _queryService;

    public QueryController(ILogger<QueryController> logger, QueryService queryService)
    {
        _logger = logger ?? throw new NullReferenceException(nameof(logger));
        _queryService = queryService ?? throw new NullReferenceException(nameof(queryService));

    }


    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var Queries = _queryService.GetQueries() ?? throw new NullReferenceException();
            return Ok(Queries);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message, exception.Source);
            return BadRequest("Error occured while processing your request");
        }
    }



    [HttpGet]
    public IActionResult GetQueriesByUserId(int UserID)
    {
        if (UserID == 0) return NotFound();
        return Ok(_queryService.GetQueriesByUserID(UserID));
    }



    [HttpGet]
    public IActionResult GetQueriesByTitle(String Title)
    {

        return Title != null ? Ok(_queryService.GetQueriesByTitle(Title)) : NotFound();

    }

    [HttpGet]
    public IActionResult GetQueries(bool IsSolved)
    {

        return Ok(_queryService.GetQueries(IsSolved));
    }

    [HttpGet]
    public IActionResult GetComments(int QueryId)
    {

        return QueryId > 0 ? Ok(_queryService.GetComments(QueryId)) : NotFound();
    }

    [HttpPost]
    public IActionResult AddQuery(Query query)
    {
        if (query == null) return BadRequest("Null value is not supported");
        try
        {
            return _queryService.AddQuery(query) ?  Ok("Successfully Created"):  BadRequest("Unable to Add - Given data is Invalid");
        }
        catch (Exception exception)
        {
           return BadRequest("Error Occured");    
        }

    }

    [HttpPost]
    public IActionResult AddComment(QueryComment comment)
    {
        if (comment == null) return BadRequest("Null value is not supported");
        return comment != null ? Ok(_queryService.AddCommentToQuery(comment)) : NoContent();

    }


}
