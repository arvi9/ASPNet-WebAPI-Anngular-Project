

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using AspireOverflow.Models;
using AspireOverflow.Services;
using AspireOverflow.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using System.Text.Json;
namespace AspireOverflow.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class QueryController : ControllerBase
    {

        internal static ILogger<QueryController> _logger;
        private static QueryService _queryService;

        public QueryController(ILogger<QueryController> logger, QueryService queryService)
        {
            _logger = logger;
            _queryService = queryService;

        }


        [HttpPost]

        public async Task<ActionResult<Query>> CreateQuery(Query query)
        {
            if (query == null) return BadRequest("Null value is not supported");
            try
            {

              
                return _queryService.CreateQuery(query) ? await Task.FromResult(Ok("Successfully Created")) : BadRequest($"Error Occured while Adding Query :{HelperService.PropertyList(query)}");
            }
            catch (ValidationException exception)
            {
                //HelperService.LoggerMessage - returns string for logger with detailed info
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryController), nameof(CreateQuery), exception, query));
                return BadRequest($"{exception.Message}\n{HelperService.PropertyList(query)}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryController), nameof(CreateQuery), exception, query));
                return BadRequest($"Error Occured while Adding Query :{HelperService.PropertyList(query)}");
            }
        }


        [HttpDelete]
        public async Task<ActionResult> RemoveQueryByQueryId(int QueryId)
        {
            if (QueryId <= 0) return BadRequest("Query ID must be greater than 0");
            try
            {
                return _queryService.RemoveQueryByQueryId(QueryId) ? await Task.FromResult(Ok($"Successfully deleted the record with QueryId :{QueryId}")) : BadRequest($"Error Occurred while removing query with QueryId :{QueryId}");
            }

            catch (ItemNotFoundException exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryController), nameof(RemoveQueryByQueryId), exception, QueryId));
                return NotFound($"{exception.Message}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryController), nameof(RemoveQueryByQueryId), exception, QueryId));
                return BadRequest($"Error Occurred while removing query with QueryId :{QueryId}");
            }
        }


        [HttpPatch]
        public async Task<ActionResult> MarkQueryAsSolved(int QueryId)
        {
            if (QueryId <= 0) return BadRequest("Query ID must be greater than 0");
            try
            {
                return _queryService.MarkQueryAsSolved(QueryId) ? await Task.FromResult(Ok($"Successfully marked as Solved Query in the record with QueryId :{QueryId}")) : BadRequest($"Error Occurred while marking query as solved with QueryId :{QueryId}");
            }

            catch (ItemNotFoundException exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryController), nameof(MarkQueryAsSolved), exception, QueryId));
                return NotFound($"{exception.Message}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryController), nameof(MarkQueryAsSolved), exception, QueryId));
                return BadRequest($"Error Occurred while marking query as solved with QueryId :{QueryId}");
            }
        }
        [HttpGet]
        public async Task<ActionResult<Query>> GetQuery(int QueryId)
        {
            if (QueryId <= 0) return BadRequest("Query ID must be greater than 0");
            try

            {
                var Query = _queryService.GetQuery(QueryId);

                return await Task.FromResult(Ok(Query));

            }
            catch (ItemNotFoundException exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryController), nameof(GetQuery), exception, QueryId));
                 return BadRequest($"{exception.Message} with QueryId:{QueryId}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryController), nameof(GetQuery), exception, QueryId));
                return BadRequest($"Error Occurred while getting query with QueryId :{QueryId}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerator<Query>>> GetAll()
        {
            try
            {

                var Queries = _queryService.GetQueries();   
                return await Task.FromResult(Ok(Queries));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryController), nameof(GetAll), exception));
                return BadRequest("Error occured while processing your request");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerator<Query>>> GetLatestQueries()
        {
            try
            {
                var Queries = _queryService.GetLatestQueries();
              
                return await Task.FromResult(Ok(Queries));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryController), nameof(GetLatestQueries), exception));
                return BadRequest("Error occured while processing your request");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetTrendingQueries()
        {
            try
            {
                var Queries = _queryService.GetTrendingQueries();
                return await Task.FromResult(Ok(Queries));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryController), nameof(GetTrendingQueries), exception));
                return BadRequest("Error occured while processing your request");
            }
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerator<Query>>> GetQueriesByUserId(int UserId)
        {
            if (UserId <= 0) return BadRequest("UserId must be greater than 0");
            try
            {
                var ListOfQueriesByUserId = _queryService.GetQueriesByUserId(UserId);
              
                return await Task.FromResult(Ok(ListOfQueriesByUserId));
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryController), nameof(GetQueriesByUserId), exception, UserId));
                return BadRequest($"Error Occured while processing your request with UserId :{UserId}");
            }

        }



        [HttpGet]
        public async Task<ActionResult<IEnumerator<Query>>> GetQueriesByTitle(string Title)
        {
            if (String.IsNullOrEmpty(Title)) return BadRequest("Title can't be null");
            try
            {
                var ListOfQueriesByTitle = _queryService.GetQueriesByTitle(Title); 
                return await Task.FromResult(Ok(ListOfQueriesByTitle));
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryController), nameof(GetQueriesByTitle), exception, Title));
                return BadRequest($"Error Occured while processing your request with Title :{Title}");

            }


        }

        [HttpGet]
        public async Task<ActionResult<IEnumerator<Query>>> GetQueriesByIsSolved(bool IsSolved)
        {

            try
            {
                var ListOfQueriesByIsSolved = _queryService.GetQueries(IsSolved);
                return await Task.FromResult(Ok(ListOfQueriesByIsSolved));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryController), nameof(GetQueriesByIsSolved), exception, IsSolved));
                return BadRequest($"Error Occured while processing your request with IsSolved :{IsSolved}");

            }

        }


        [HttpPost]
        public async Task<ActionResult<QueryComment>> CreateComment(QueryComment comment)
        {

            if (comment == null) return BadRequest("Null value is not supported");
            try
            {
                return _queryService.CreateComment(comment) ? await Task.FromResult(Ok("Successfully added comment to the Query")) : BadRequest($"Error Occured while Adding Comment :{HelperService.PropertyList(comment)}");
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryController), nameof(CreateComment), exception, comment));
                return BadRequest($"{exception.Message}\n{HelperService.PropertyList(comment)}");
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryController), nameof(CreateComment), exception, comment));
                return BadRequest($"Error Occured while Adding comment :{HelperService.PropertyList(comment)}");
            }

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerator<QueryComment>>> GetComments(int QueryId)
        {
            if (QueryId <= 0) return BadRequest("QueryId must be greater than 0");
            try
            {
                var ListOfComments = _queryService.GetComments(QueryId);
                return await Task.FromResult(Ok(ListOfComments));
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryController), nameof(GetComments), exception, QueryId));
                return BadRequest($"Error Occured while processing your request with QueryId :{QueryId}");


            }

        }

    }
}
