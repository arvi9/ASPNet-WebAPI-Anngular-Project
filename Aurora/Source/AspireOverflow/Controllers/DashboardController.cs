using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using AspireOverflow.Models;
using AspireOverflow.Services;

using Microsoft.AspNetCore.Authorization;

namespace AspireOverflow.Controllers
{

    [ApiController][Authorize]
    [Route("[controller]/[action]")]
    public class DashboardController : BaseController
    {

        internal ILogger<DashboardController> _logger;
        private ArticleService _articleService;
        private QueryService _queryService;
        private UserService _userService;
        public DashboardController(ILogger<DashboardController> logger, ArticleService articleService, QueryService queryService, UserService userService)
        {
            _logger = logger;
            _articleService = articleService;
            _userService = userService;
            _queryService = queryService;


        }


        [HttpGet]

        public async Task<ActionResult> GetReviewerDashboard(int ReviewerId) //reviewerID temporarily getting as input ,later it is retrived from claims.
        {
            try
            {
                var DashboardInformation = new
                {
                    TotalNumberOfArticles = _articleService.GetListOfArticles().Count(),
                    ArticlesTobeReviewed = _articleService.GetArticlesByArticleStatusId(2).Count(),
                    ArticlesReviewed = _articleService.GetArticlesByReviewerId(ReviewerId).Count(),

                }; return await Task.FromResult(Ok(DashboardInformation));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("DashboardController", "GetReviewerDashboard(int ReviewerId)", exception));
                return BadRequest("Error Occured while processing your request");
            }
        }

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