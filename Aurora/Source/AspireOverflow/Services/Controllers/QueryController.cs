using System.Net;
using Microsoft.AspNetCore.Mvc;
using AspireOverflow.Models;

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

        return QueryService.AddQuery(query);
    }

    [HttpPost]
    public HttpStatusCode AddComment(QueryComment comment)
    {

        return QueryService.AddCommentToQuery(comment);
    }


}
