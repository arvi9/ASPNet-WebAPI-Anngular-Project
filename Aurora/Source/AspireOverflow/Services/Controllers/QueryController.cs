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

    public QueryController(ILogger<QueryController> logger)
    {
        _logger = logger;



    }


    [HttpGet]
    public IActionResult GetAll()
    {
        _logger.LogInformation("logging in GetAll method");
        return Ok(QueryService.GetQueries());

        
    }



    [HttpGet]
    public IActionResult GetQueriesByUserId(int UserID)
    {
        if(UserID ==0)return NotFound();
        return Ok(QueryService.GetQueriesByUserID(UserID));
    }

     

    [HttpGet]
    public IActionResult GetQueriesByTitle(String Title)
    {
       
            return Ok(QueryService.GetQueriesByTitle(Title));
       
    }

    [HttpGet]
    public IActionResult GetQueries(bool IsSolved)
    {

        return Ok(QueryService.GetQueries(IsSolved));
    }

    [HttpGet]
    public IActionResult GetComments(int QueryId)
    {

        return QueryId > 0 ? Ok(QueryService.GetComments(QueryId)):NotFound();
    }

    [HttpPost]
    public IActionResult AddQuery(Query query)
    {
        return query != null ? Ok(QueryService.AddQuery(query)) : NoContent();

    }

    [HttpPost]
    public IActionResult AddComment(QueryComment comment)
    {

        return comment != null ? Ok(QueryService.AddCommentToQuery(comment)) : NoContent();

    }


}
