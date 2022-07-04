using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using AspireOverflow.Models;
using AspireOverflow.Services;

using Microsoft.AspNetCore.Authorization;
using AspireOverflow.DataAccessLayer.Interfaces;

namespace AspireOverflow.Controllers
{

    [ApiController][Authorize]
    [Route("[controller]/[action]")]
    public class DashboardController : BaseController
    {

        private readonly ILogger<DashboardController> _logger;
        private readonly IArticleService _articleService;
        private readonly IQueryService _queryService;
        private readonly IUserService _userService;
        public DashboardController(ILogger<DashboardController> logger, IArticleService articleService, IQueryService queryService, IUserService userService)
        {
            _logger = logger;
            _articleService = articleService;
            _userService = userService;
            _queryService = queryService;


        }
        /// <summary>
        /// Gets a reviewer dashboard by reviewer id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Dashboard/GetReviewerDashboard
        /// 
        ///  * fields are required
        /// 
        ///     body
        ///     {
        ///         ReviewerId*: int,
        ///     }
        /// </remarks>
        /// <response code="200">Returns a reviewer dashboard by reviewer id. </response>
          /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <param name="ReviewerId"></param>

        [HttpGet]
        public async Task<ActionResult> GetReviewerDashboard() //reviewerID temporarily getting as input ,later it is retrived from claims.
        {
            try
            {int ReviewerId=GetCurrentUser().UserId;
                var DashboardInformation = new
                {
                    TotalNumberOfArticles = _articleService.GetAll().Count(),
                    ArticlesTobeReviewed = _articleService.GetArticlesByArticleStatusId(2).Count(),
                    ArticlesReviewed =_articleService.GetArticlesByArticleStatusId(4).Count(),
                    ArticlesPublished = _articleService.GetArticlesByReviewerId(ReviewerId).Count(),


                }; return await Task.FromResult(Ok(DashboardInformation));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("DashboardController", "GetReviewerDashboard(int ReviewerId)", exception));
                return BadRequest("Error Occured while processing your request");
            }
        }

        /// <summary>
        /// Gets admin dashboard.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Dashboard/GetAdminDashboard
        ///
        /// </remarks>
        /// <response code="200">Returns a admin dashboard. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>

        [HttpGet]

        public async Task<ActionResult> GetAdminDashboard()
        {
            try
            {
                var DashboardInformation = new
                {
                    TotalNumberOfArticles = _articleService.GetAll().Count(),
                    TotalNumberOfUsers = _userService.GetUsers().Count(),
                    TotalNumberOfReviwers = _userService.GetUsersByIsReviewer(true).Count(),
                    TotalNumberofQueries = _queryService.GetListOfQueries().Count(),
                    TotaNumberofSolvedQueries = _queryService.GetQueries(true).Count(),
                    TotalNumberOfUnSolvedQueries = _queryService.GetQueries(false).Count(),

                }; return await Task.FromResult(Ok(DashboardInformation));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("DashboardController", "GetAdminDashboard()", exception));
                return BadRequest("Error Occured while processing your request");
            }
        }

        /// <summary>
        /// Gets home page.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Dashboard/GetHomePage
        ///
        /// </remarks>
        /// <response code="200">Returns a homepage. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        
        [HttpGet]

        public async Task<ActionResult> GetHomePage()
        {
            try
            {
                var Data = new
                {
                    TrendingArticles = _articleService.GetTrendingArticles(),
                    LatestArticles = _articleService.GetLatestArticles(),
                    TrendingQueries = _queryService.GetTrendingQueries(),
                    LatestQueries = _queryService.GetLatestQueries(),


                }; return await Task.FromResult(Ok(Data));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("DashboardController", "GetHomePage()", exception));
                return BadRequest("Error Occured while processing your request");
            }
        }



    }
}