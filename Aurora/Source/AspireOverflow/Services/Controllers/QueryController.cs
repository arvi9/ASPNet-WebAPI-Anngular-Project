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

    public QueryController(ILogger<QueryController> logger,QueryService queryService)
    {
        _logger = logger;
        _queryService=queryService;



    }


    [HttpGet]
    public IActionResult GetAll()
    {
        _logger.LogInformation("logging in GetAll method");
        return Ok(_queryService.GetQueries());

        
    }



    [HttpGet]
    public IActionResult GetQueriesByUserId(int UserID)
    {
        if(UserID ==0)return NotFound();
        return Ok(_queryService.GetQueriesByUserID(UserID));
    }

     

    [HttpGet]
    public IActionResult GetQueriesByTitle(String Title)
    {
       
            return Title != null ? Ok(_queryService.GetQueriesByTitle(Title)):NotFound();
       
    }

    [HttpGet]
    public IActionResult GetQueries(bool IsSolved)
    {

        return Ok(_queryService.GetQueries(IsSolved));
    }

    [HttpGet]
    public IActionResult GetComments(int QueryId)
    {

        return QueryId > 0 ? Ok(_queryService.GetComments(QueryId)):NotFound();
    }

    [HttpPost]
    public IActionResult AddQuery(Query query)
    {
        return query != null ? Ok(_queryService.AddQuery(query)) : NoContent();

    }

    [HttpPost]
    public IActionResult AddComment(QueryComment comment)
    {

        return comment != null ? Ok(_queryService.AddCommentToQuery(comment)) : NoContent();

    }


}
