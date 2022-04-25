using System.Net;
using Microsoft.AspNetCore.Mvc;
using AspireOverflow.Models;
using System;

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
    public IEnumerable<Query> GetAll()
    {
        _logger.LogInformation("logging in GetAll method");
        return QueryService.GetQueries();
    }



    [HttpGet]
    public IEnumerable<Query> GetQueriesByUserId(int UserID)
    {

        return QueryService.GetQueriesByUserID(UserID);
    }


    [HttpGet]
    public IEnumerable<Query> GetQueriesByTitle(String Title)
    {
       
            return QueryService.GetQueriesByTitle(Title);
       
    }

    [HttpGet]
    public IEnumerable<Query> GetQueries(bool IsSolved)
    {

        return QueryService.GetQueries(IsSolved);
    }

    [HttpGet]
    public IEnumerable<QueryComment> GetComments(int QueryId)
    {

        return QueryService.GetComments(QueryId);
    }

    [HttpPost]
    public HttpStatusCode AddQuery(Query query)
    {
        return query != null ? QueryService.AddQuery(query) : HttpStatusCode.NotAcceptable;

    }

    [HttpPost]
    public HttpStatusCode AddComment(QueryComment comment)
    {

        return comment != null ? QueryService.AddCommentToQuery(comment) : HttpStatusCode.NotAcceptable;

    }


}
