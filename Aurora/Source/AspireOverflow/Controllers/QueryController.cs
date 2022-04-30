using System.Data.SqlTypes;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
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


    [HttpPost]
    public IActionResult CreateQuery(Query query)
    {
        if (query == null) return BadRequest("Null value is not supported");

        try
        {

            return _queryService.CreateQuery(query, DevelopmentTeam.Web) ? Created("Successfully Created",query) : BadRequest($"Error Occured while Adding Query :{HelperService.PropertyList(query)}");
        }
        catch (ValidationException exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(CreateQuery), exception, query));
            return BadRequest($"{exception.Message}\n{HelperService.PropertyList(query)} ");

        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(CreateQuery), exception, query));
            return BadRequest($"Error Occured while Adding Query :{HelperService.PropertyList(query)}");
        }

    }

    [HttpPost]
    public IActionResult CreateComment(QueryComment comment)
    {

        if (comment == null) return BadRequest("Null value is not supported");
        try
        {
            return _queryService.CreateComment(comment, DevelopmentTeam.Web) ? Created("Successfully Created",comment) : BadRequest($"Error Occured while Adding Comment :{HelperService.PropertyList(comment)}");
        }

        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(CreateComment), exception, comment));
            return BadRequest($"Error Occured while Adding comment :{HelperService.PropertyList(comment)}");
        }

    }

    [HttpDelete]
    public IActionResult RemoveQueryByQueryId(int QueryId)
    {
        Validation.ValidateId(QueryId);
        try
        {
            return _queryService.RemoveQueryByQueryId(QueryId, DevelopmentTeam.Web) ? Ok($"Successfully deleted the record with QueryId :{QueryId}") : BadRequest($"Error  Occured with QueryId :{QueryId}");
        }
        catch (ArgumentOutOfRangeException exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(RemoveQueryByQueryId), exception, QueryId));
            return BadRequest($"{exception.Message} with QueryId :{QueryId}");
        }
        catch (SqlNullValueException exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(RemoveQueryByQueryId), exception, QueryId));
            return NotFound($"{exception.Message} with QueryId :{QueryId}");
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(RemoveQueryByQueryId), exception, QueryId));
            return BadRequest($"Error  Occured with QueryId :{QueryId}");
        }
    }


    [HttpPatch]
    public IActionResult MarkQueryAsSolved(int QueryId)
    {
        Validation.ValidateId(QueryId);
        try
        {
            return _queryService.MarkQueryAsSolved(QueryId, DevelopmentTeam.Web) ? Ok($"Successfully marked as Solved Query in  the record with QueryId :{QueryId}") : BadRequest($"Error  Occured with QueryId :{QueryId}");
        }
        catch (ArgumentOutOfRangeException exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(MarkQueryAsSolved), exception, QueryId));
            return BadRequest($"{exception.Message} with QueryId :{QueryId}");
        }
        catch (SqlNullValueException exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(MarkQueryAsSolved), exception, QueryId));
            return NotFound($"{exception.Message} with QueryId :{QueryId}");
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(MarkQueryAsSolved), exception, QueryId));
            return BadRequest($"Error  Occured with QueryId :{QueryId}");
        }
    }
    [HttpGet]
    public IActionResult GetQuery(int QueryId)
    {
        Validation.ValidateId(QueryId);
        try
        {

            return Ok(_queryService.GetQuery(QueryId, DevelopmentTeam.Web));
        }
        catch (ArgumentOutOfRangeException exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(GetQuery), exception, QueryId));
            return BadRequest($"{exception.Message} with QueryId :{QueryId}");
        }
        catch (SqlNullValueException exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(GetQuery), exception, QueryId));
            return BadRequest($"{exception.Message} with QueryId :{QueryId}");
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(GetQuery), exception, QueryId));
            return BadRequest($"Error  Occured with QueryId :{QueryId}");
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {

            var Queries = _queryService.GetQueries(DevelopmentTeam.Web);    // DevelopmentTeam.Web is a property of enum class
            return Ok(Queries);
        }

        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(GetAll), exception));
            return BadRequest("Error occured while processing your request");
        }
    }



    [HttpGet]
    public IActionResult GetQueriesByUserId(int UserId)
    {
        if (UserId <= 0) return BadRequest("UserId must be greater than 0");
        try
        {
            var ListOfQueriesByUserId = _queryService.GetQueries(DevelopmentTeam.Web);
            return Ok(ListOfQueriesByUserId);
        }

        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(GetQueriesByUserId), exception, UserId));
            return BadRequest($"Error Occured while processing your request with UserId :{UserId}");
        }

    }



    [HttpGet]
    public IActionResult GetQueriesByTitle(string Title)
    {
        if (String.IsNullOrEmpty(Title)) return BadRequest("Title can't be null");
        try
        {
            var ListOfQueriesByTitle = _queryService.GetQueriesByTitle(Title, DevelopmentTeam.Web);
            return Ok(ListOfQueriesByTitle);
        }

        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(GetQueriesByTitle), exception, Title));
            return BadRequest($"Error Occured while processing your request with Title :{Title}");

        }


    }

    [HttpGet]
    public IActionResult GetQueries(bool IsSolved)
    {

        try
        {
            var ListOfQueriesByIsSolved = _queryService.GetQueries(IsSolved, DevelopmentTeam.Web);
            return Ok(ListOfQueriesByIsSolved);
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(GetQueries), exception, IsSolved));
            return BadRequest($"Error Occured while processing your request with IsSolved :{IsSolved}");

        }

    }



    [HttpGet]
    public IActionResult GetComments(int QueryId)
    {
        if (QueryId <= 0) return BadRequest("QueryId must be greater than 0");
        try
        {
            var ListOfComments = _queryService.GetComments(QueryId, DevelopmentTeam.Web);
            return Ok(ListOfComments);
        }

        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(GetComments), exception, QueryId));
            return BadRequest($"Error Occured while processing your request with QueryId :{QueryId}");


        }

    }

}
