using System.Net;
using Microsoft.AspNetCore.Mvc;
using AspireOverflow.Models;

namespace AspireOverflow.Controllers;

[ApiController]
[Route("[controller]")]
public class QueryController : ControllerBase
{
    
    internal static  ILogger<QueryController> _logger;

    public QueryController(ILogger<QueryController> logger)
    {
        _logger = logger;
        
   

    }
    

    [HttpGet]
    public IEnumerable<Query> GetAll()
    {
       
        return QueryService.GetQueries();
    }

    [HttpPost]

    public HttpStatusCode AddQuery(Query query){
      
        return QueryService.AddQuery(query);
    }


}
