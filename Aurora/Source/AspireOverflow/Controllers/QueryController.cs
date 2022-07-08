using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using AspireOverflow.Models;
using AspireOverflow.Services;
using AspireOverflow.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using AspireOverflow.DataAccessLayer.Interfaces;
namespace AspireOverflow.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class QueryController : BaseController
    {
        private readonly ILogger<QueryController> _logger;
        private readonly IQueryService _queryService;
        public QueryController(ILogger<QueryController> logger, IQueryService queryService)
        {
            _logger = logger;
            _queryService = queryService;
        }


        /// <summary>
        /// Create a query.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Query/CreateQuery
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         queryId*: int,
        ///         title*: string,
        ///         content*: string,
        ///         code*: string,
        ///         isSolved*: boolean,
        ///         isActive*: boolean,
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">If the query was created. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="query"></param>
        [HttpPost]
        public async Task<ActionResult> CreateQuery(Query query)
        {
            if (query == null) return BadRequest(Message("Null value is not supported"));
            try
            {
               query.CreatedBy=GetCurrentUser().UserId;
                return _queryService.CreateQuery(query) ? await Task.FromResult(Ok(Message("Successfully Created"))) : BadRequest(Message($"Error Occured while Adding Query :{HelperService.PropertyList(query)}"));
            }
            catch (ValidationException exception)
            {
                //HelperService.LoggerMessage - returns string for logger with detailed info
                _logger.LogError(HelperService.LoggerMessage("QueryController", "CreateQuery(Query query)", exception, query));
                return BadRequest(Message($"{exception.Message}",query));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", "CreateQuery(Query query)", exception, query));
                return Problem($"Error Occured while Adding Query :{HelperService.PropertyList(query)}");
            }
        }


        /// <summary>
        /// To remove query by its id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Query/RemoveQueryByQueryId
        ///     
        ///     * fields are required
        /// 
        ///     body
        ///             {
        ///                 QueryId* : int,
        ///                
        ///             }
        /// 
        /// </remarks>
        /// <response code="200">Returns a removed query successfully. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If query was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="QueryId"></param>
        [HttpDelete]
        public async Task<ActionResult> RemoveQueryByQueryId(int QueryId)
        {
            if (QueryId <= 0) return BadRequest(Message("Query ID must be greater than 0"));
            try
            {
                return _queryService.RemoveQueryByQueryId(QueryId) ? await Task.FromResult(Ok($"Successfully deleted the record with QueryId :{QueryId}")) : BadRequest(Message($"Error Occurred while removing query with QueryId :{QueryId}"));
            }
            catch (ItemNotFoundException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", "RemoveQueryByQueryId(int QueryId)", exception, QueryId));
                return NotFound($"{exception.Message}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", "RemoveQueryByQueryId(int QueryId)", exception, QueryId));
                return Problem($"Error Occurred while removing query with QueryId :{QueryId}");
            }
        }


        [HttpPatch]
        public async Task<ActionResult> MarkQueryAsSolved(int QueryId)
        {
            if (QueryId <= 0) return BadRequest(Message("Query ID must be greater than 0"));
            try
            {
                return _queryService.MarkQueryAsSolved(QueryId) ? await Task.FromResult(Ok($"Successfully marked as Solved Query in the record with QueryId :{QueryId}")) : BadRequest(Message($"Error Occurred while marking query as solved with QueryId :{QueryId}"));
            }
            catch (ItemNotFoundException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", "MarkQueryAsSolved(int QueryId)", exception, QueryId));
                return NotFound($"{exception.Message}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", "MarkQueryAsSolved(int QueryId)", exception, QueryId));
                return Problem($"Error Occurred while marking query as solved with QueryId :{QueryId}");
            }
        }


        /// <summary>
        /// Gets a query by its id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Query/GetQuery
        ///
        /// </remarks>
        /// <response code="200">Returns a query that specify to its id.. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="QueryId"></param>
        [HttpGet]
        public async Task<ActionResult> GetQuery(int QueryId)
        {
            if (QueryId <= 0) return BadRequest(Message("Query ID must be greater than 0"));
            try
            {
                var Query = _queryService.GetQuery(QueryId);
                return await Task.FromResult(Ok(Query));
            }
            catch (ItemNotFoundException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", "GetQuery(int QueryId)", exception, QueryId));
                return Problem($"{exception.Message} with QueryId:{QueryId}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", "GetQuery(int QueryId)", exception, QueryId));
                return BadRequest(Message($"Error Occurred while getting query with QueryId :{QueryId}"));
            }
        }


        /// <summary>
        /// Gets all queries.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Query/GetAll
        ///
        /// </remarks>
        /// <response code="200">Returns a list of queries. </response>
        /// <response code="500">If there is problem in server. </response>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var Queries = _queryService.GetListOfQueries();
                return await Task.FromResult(Ok(Queries));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", "GetAll()", exception));
                return Problem("Error occured while processing your request");
            }
        }


        /// <summary>
        /// Get Latest Queries
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Query/GetLatestQueries
        ///
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         Range*: int,
        ///     }
        /// </remarks>
        /// <response code="200">Returns a latest queries in a queries. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="Range"></param>
        /// 
        [HttpGet]
        public async Task<ActionResult> GetLatestQueries(int Range = 0)
        {
            try
            {
                var Queries = _queryService.GetLatestQueries().ToList();
                if (Range <= Queries.Count)
                {
                    Queries = Range > 0 ? Queries.GetRange(1, Range) : Queries;
                    return await Task.FromResult(Ok(Queries));
                }
                else return BadRequest(Message("Range limit exceeded"));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", "GetLatestQueries(int Range=0)", exception));
                return Problem("Error occured while processing your request");
            }
        }


        /// <summary>
        /// Get Trending Queries.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Query/GetTrendingQueries?Range=0
        ///
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         "Range": 0,
        ///     }
        /// </remarks>
        /// <response code="200">Returns a trending queries in a queries. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        [HttpGet]
        public async Task<ActionResult> GetTrendingQueries(int Range = 0)
        {
            try
            {
               List<object> Queries = _queryService.GetTrendingQueries().ToList();
                if (Range <= Queries.Count)
                {
                    Queries = Range > 0 ? Queries.GetRange(0, Range) : Queries;
                    return await Task.FromResult(Ok(Queries));
                }
                else return BadRequest(Message("Range limit exceeded"));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", "GetTrendingQueries(int Range=0)", exception));
                return Problem("Error occured while processing your request");
            }
        }


        [HttpGet]
        public async Task<ActionResult> GetQueriesByUserId()
        {
            int UserId=GetCurrentUser().UserId;
            try
            {
                var ListOfQueriesByUserId = _queryService.GetQueriesByUserId(UserId);
                return await Task.FromResult(Ok(ListOfQueriesByUserId));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", "GetQueriesByUserId(int UserId)", exception, UserId));
                return Problem($"Error Occured while processing your request with UserId :{UserId}");
            }
        }


        [HttpGet]
        public async Task<ActionResult> GetQueriesByTitle(string Title)
        {
            if (String.IsNullOrEmpty(Title)) return BadRequest(Message("Title can't be null"));
            try
            {
                var ListOfQueriesByTitle = _queryService.GetQueriesByTitle(Title);
                return await Task.FromResult(Ok(ListOfQueriesByTitle));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", "GetQueriesByTitle(string Title)", exception, Title));
                return Problem($"Error Occured while processing your request with Title :{Title}");
            }
        }


        /// <summary>
        /// Gets a list of solved queries in query.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Query/GetQueriesByIsSolved
        ///
         ///  * fields are required
        /// 
        ///     body
        ///     {
        ///         IsSolved*: bool,
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Returns a list of solved queries.</response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="IsSolved"></param>
        [HttpGet]
        public async Task<ActionResult> GetQueriesByIsSolved(bool IsSolved)
        {
            try
            {
                var ListOfQueriesByIsSolved = _queryService.GetQueries(IsSolved);
                return await Task.FromResult(Ok(ListOfQueriesByIsSolved));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", "GetQueriesByIsSolved(bool IsSolved)", exception, IsSolved));
                return Problem($"Error Occured while processing your request with IsSolved :{IsSolved}");
            }
        }


        /// <summary>
        /// Create a comment
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Article/CreateComment
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         QueryCommentId*: int,
        ///         comment*: string,
        ///         datetime*: 2022-06-18T09:23:35.622Z,
        ///         code*: string,
        ///         queryId*: int,
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">If the comment was created successfully. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="comment"></param>
        [HttpPost]
        public async Task<ActionResult> CreateComment(QueryComment comment)
        {
            if (comment == null) return BadRequest(Message("Null value is not supported"));
            try
            {
                comment.CreatedBy=GetCurrentUser().UserId;
                return _queryService.CreateComment(comment) ? await Task.FromResult(Ok(Message("Successfully added comment to the Query"))) : BadRequest(Message($"Error Occured while Adding Comment :{HelperService.PropertyList(comment)}"));
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", " CreateComment(QueryComment comment)", exception, comment));
                return BadRequest(Message($"{exception.Message}",comment));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", " CreateComment(QueryComment comment)", exception, comment));
                return Problem($"Error Occured while Adding comment :{HelperService.PropertyList(comment)}");
            }
        }


        /// <summary>
        /// Gets a list of comments by query id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///    url: https://localhost:7197/Query/GetComments
        /// 
        ///  * fields are required
        /// 
        ///     body
        ///     {
        ///         QueryId*: int,
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns a list of comments by query id.. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="QueryId"></param>
        [HttpGet]
        public async Task<ActionResult> GetComments(int QueryId)
        {
            if (QueryId <= 0) return BadRequest(Message("QueryId must be greater than 0"));
            try
            {
                var ListOfComments = _queryService.GetComments(QueryId);
                return await Task.FromResult(Ok(ListOfComments));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", " GetComments(int QueryId)", exception, QueryId));
                return Problem($"Error Occured while processing your request with QueryId :{QueryId}");
            }
        }


        /// <summary>
        /// Add a spam 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Query/AddSpam
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///            spamId*: int,
        ///            reason*: string,
        ///            queryId*: int,
        ///            userId*: int,
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">If the spam was added. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="spam"></param>
        [HttpPost]
        public async Task<ActionResult> AddSpam(Spam spam)
        {
            if (spam == null ) return BadRequest(Message("spam object cannot be null"));
            try
            {
                spam.UserId=GetCurrentUser().UserId;
                return _queryService.AddSpam( spam) ? await Task.FromResult(Ok(Message("Successfully added spam for the query"))) : BadRequest(Message("Error Occured while adding spam to the query "));
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", "AddSpam(int QueryId)", exception, spam));
                return Problem($"{exception.Message}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", "AddSpam(int Query)", exception, spam));
                return BadRequest(Message("Error Occurred while Adding Spam with Spam data",spam));
            }
        }


        /// <summary>
        /// Gets a list of spam.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Query/GetListOfSpams
        ///
        /// </remarks>
        /// <response code="200">Returns a list of spams. </response>
        /// <response code="500">If there is problem in server. </response>
        [HttpGet]
        public async Task<ActionResult> GetListOfSpams()
        {
            try
            {
                var ListOfSpams = _queryService.GetSpams(3); //verify status ->1-approved,2->Rejected,3->Not verified
                return await Task.FromResult(Ok(ListOfSpams));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryController", "GetListOfSpams()", exception));
                return Problem("Error occured while processing your request");
            }
        }


        /// <summary>
        /// To update the spam status
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Query/UpdateSpamStatus
        ///
         ///     * fields are required
        /// 
        ///     body
        ///     {
        ///            spamId*: int,
        ///            VerifyStatusId*: int,
        ///     } 
        /// 
        /// </remarks>
        /// <response code="200">If the spam status was updated </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="SpamId"></param>
        /// <param name="VerifyStatusID"></param>
    [HttpPatch]
    public ActionResult UpdateSpamStatus(int QueryId, int VerifyStatusID)
    {
        if (QueryId <= 0) return BadRequest(Message($"QueryId Id must be greater than 0  where QueryId:{QueryId}"));
        if (VerifyStatusID <= 0 || VerifyStatusID > 3) return BadRequest(Message($"VerifyStatusId must be greater than 0  and less than 3 where VerifyStatusID:{VerifyStatusID}"));
        try
        {
            return _queryService.ChangeSpamStatus(QueryId, VerifyStatusID)? Ok(Message("successfully Updated")): BadRequest(Message($"Error occured while processing your request with QueryId:{QueryId} and VerifyStatusId:{VerifyStatusID}"));
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage($"QueryService", "ChangeSpamStatus(int QueryId, int VerifyStatusID)", exception, QueryId, VerifyStatusID));
            return Problem($"Error occured while processing your request with QueryId:{QueryId} and VerifyStatusId:{VerifyStatusID}");
        }
    }
    }
}
