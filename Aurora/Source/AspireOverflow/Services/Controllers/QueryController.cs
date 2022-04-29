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
            _logger.LogError($"{exception.Message},{exception.StackTrace}");
            return BadRequest("Error occured while processing your request");
        }
    }



    [HttpGet]
    public IActionResult GetQueriesByUserId(int UserID)
    {
        if (UserID == 0) return BadRequest("UserId must be greater than 0");
        try
        {
            var ListOfQueriesByUserId = _queryService.GetQueriesByUserID(UserID);
            return Ok(ListOfQueriesByUserId);
        }
        catch (ArgumentOutOfRangeException exception)
        {
            _logger.LogError($"{exception.Message},{exception.StackTrace}");
            return BadRequest("UserId must be greater than 0");
        }
        catch (Exception exception)
        {
            _logger.LogError($"{exception.Message},{exception.StackTrace}");
            return BadRequest("Error Occured while processing your request");
        }

    }



    [HttpGet]
    public IActionResult GetQueriesByTitle(String Title)
    {
        if (Title == null) return BadRequest("Title can't be null");
        try
        {
            var ListOfQueriesByTitle = _queryService.GetQueriesByTitle(Title);
            return Ok(ListOfQueriesByTitle);
        }
        catch (ArgumentNullException exception)
        {
            _logger.LogError($"{exception.Message},{exception.StackTrace}");
            return BadRequest("Title can't be null");
        }
        catch (Exception exception)
        {
            _logger.LogError($"{exception.Message},{exception.StackTrace}");
            return BadRequest("Error Occured while processing your request");

        }


    }

    [HttpGet]
    public IActionResult GetQueries(bool IsSolved)
    {

        try
        {
            var ListOfQueriesByIsSolved = _queryService.GetQueries(IsSolved);
            return Ok(ListOfQueriesByIsSolved);
        }
        catch (Exception exception)
        {
            _logger.LogError($"{exception.Message},{exception.StackTrace}");
            return BadRequest("Error Occured while processing your request");

        }

    }



    [HttpGet]
    public IActionResult GetComments(int QueryId)
    {
        if (QueryId <= 0) return BadRequest("QueryId must be greater than 0");
        try
        {
            var ListOfComments = _queryService.GetComments(QueryId);
            return Ok(ListOfComments);
        }
        catch (ArgumentOutOfRangeException exception)
        {
            _logger.LogError($"{exception.Message},{exception.StackTrace}");
            return BadRequest("QueryId must be greater than 0");
        }
        catch (Exception exception)
        {
            _logger.LogError($"{exception.Message},{exception.StackTrace}");
            return BadRequest("Error Occured while processing your request");


        }

    }

    [HttpPost]
    public IActionResult AddQuery(Query query)
    {
        if (query == null) return BadRequest("Null value is not supported");

        try
        {

            return _queryService.AddQuery(query) ? Ok("Successfully Created") : BadRequest("Unable to Add - Given data is Invalid");
        }
        catch (ArgumentNullException exception)
        {
            _logger.LogError($"{exception.Message},{exception.StackTrace}");
            return BadRequest("Null value is not supported");
        }
        catch (Exception exception)
        {
            _logger.LogError($"{exception.Message},{exception.StackTrace}");
            return BadRequest("Error Occured");
        }

    }

    [HttpPost]
    public IActionResult AddComment(QueryComment comment)
    {

        if (comment == null) return BadRequest("Null value is not supported");
        try
        {
            return _queryService.AddCommentToQuery(comment) ? Ok("Successfully Created") : BadRequest("Unable to Add - Given data is Invalid");
        }
        catch (ArgumentNullException exception)
        {
            _logger.LogError($"{exception.Message},{exception.StackTrace}");
            return BadRequest("Null value is not supported");
        }
        catch (Exception exception)
        {
            _logger.LogError($"{exception.Message},{exception.StackTrace}");
            return BadRequest("Error Occured");
        }

    }


}
