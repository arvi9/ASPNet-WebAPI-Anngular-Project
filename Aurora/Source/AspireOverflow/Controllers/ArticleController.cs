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
    [Authorize]
    [Route("[controller]/[action]")]
    public class ArticleController : BaseController
    {
        private readonly ILogger<ArticleController> _logger;
        private readonly IArticleService _articleService;
        public ArticleController(ILogger<ArticleController> logger, IArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }
        
        /// <summary>
        ///  Create Article
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///    url : https://localhost:7197/Article/CreateArticle
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         articleId*: int,
        ///         title*: string,
        ///         content*: string,
        ///         image*: string,
        ///         articleStatusID*: int,
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the article was created. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="article"></param>
       
        [HttpPost]
        public async Task<ActionResult> CreateArticle(Article article)
        {
            if (article == null) return BadRequest(Message("Null value is not supported"));
            try
            {
              
                article.CreatedBy = GetCurrentUser().UserId;
                return _articleService.CreateArticle(article) ? await Task.FromResult(Ok(Message("Successfully Created"))) : BadRequest(Message($"Error Occured while Adding Article ", article));
            }
            catch (ValidationException exception)
            {
                //HelperService.LoggerMessage - returns string for logger with detailed info
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "CreateArticle(Article article)", exception, article));
                return BadRequest(Message(exception.Message, article));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "CreateArticle(Article article)", exception, article));
                return Problem($"Error Occured while Adding Article :{HelperService.PropertyList(article)}");
            }
        }


        /// <summary>
        /// Create Private Article
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Article/CreatePrivateArticle
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         artileId*: int,
        ///         title*: string,
        ///         content*: string,
        ///         image*: string,
        ///         datetime*: 2022-06-17T05:49:09.096Z,
        ///         Private*: true,
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the privateArticleDto was Created. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="privateArticleDto"></param>
        
        [HttpPost]
        public async Task<ActionResult> CreatePrivateArticle(PrivateArticleDto privateArticleDto)
        {
            if (privateArticleDto == null) return BadRequest(Message("Null value is not supported"));
            try
            {
                privateArticleDto.article!.CreatedBy = GetCurrentUser().UserId;
                return _articleService.CreateArticle(privateArticleDto.article, privateArticleDto.SharedUsersId!) ? await Task.FromResult(Ok(Message("Successfully Created"))) : BadRequest(Message($"Error Occured while Adding private Article ", privateArticleDto));
            }
            catch (ValidationException exception)
            {
                //HelperService.LoggerMessage - returns string for logger with detailed info
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "CreatePrivateArticle(PrivateArticleDto article)", exception, privateArticleDto));
                return BadRequest(Message(exception.Message, privateArticleDto));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "CreatePrivateArticle(PrivateArticleDto article)", exception, privateArticleDto));
                return Problem($"Error Occured while Adding private Article");
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
        ///         comment*: string,
        ///         userId*: int,
        ///         articleId*: int,
        ///         articleCommentId*: int,
        ///         datetime*: 2022-06-17T06:04:24.371Z,
        ///         
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">If the comment was created. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="comment"></param>
        [HttpPost]
        public async Task<ActionResult> CreateComment(ArticleComment comment)
        {
            if (comment == null) return BadRequest(Message("Null value is not supported"));
            try
            {
                comment.UserId = GetCurrentUser().UserId;
                return _articleService.CreateComment(comment) ? await Task.FromResult(Ok(Message("Successfully added comment to the Article"))) : BadRequest(Message($"Error Occured while Adding Comment :{HelperService.PropertyList(comment)}"));
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "CreateComment(ArticleComment comment)", exception, comment));
                return BadRequest(Message(exception.Message, comment));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "CreateComment(ArticleComment comment)", exception, comment));
                return Problem($"Error Occured while Adding comment :{HelperService.PropertyList(comment)}");
            }
        }


        /// <summary>
        /// Add Like to Article
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Article/AddLikeToArticle
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         likeId*: int,
        ///         articleId*: int,
        ///         userId*: int,
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the Like was added. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="Like"></param>
        [HttpPost]
        public async Task<ActionResult> AddLikeToArticle(ArticleLike Like)
        {
            if (Like == null) return BadRequest(Message("ArticleLike cannot be null"));
            if (Like.ArticleId <= 0) return BadRequest(Message("Article ID must be greater than 0"));
            Like.UserId = GetCurrentUser().UserId;
            try
            {
                if (!_articleService.AddLikeToArticle(Like)) return BadRequest(Message("Error Occured while adding Like to article "));
                var Result = new { message = "Successfully added Like to article", LikesCount = _articleService.GetLikesCount(Like.ArticleId) };
                return await Task.FromResult(Ok(Result));
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "AddLikeToArticle(ArticleLike Like)", exception, Like));
                return BadRequest(Message($"{exception.Message}"));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "AddLikeToArticle(ArticleLike Like)", exception, Like));
                return Problem($"Error Occurred while Adding Like  :{Like}");
            }
        }


        /// <summary>
        /// Update a article
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Article/UpdateArticle
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         artileId*: int,
        ///         title*: string,
        ///         content*: string,
        ///         image*: string
        ///         articleStatusID*: int,
        ///         reviewerId*: int,
        ///         datetime*: "2022-06-22T04:00:01.462Z",
        ///         isPrivate*: bool,
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the article was updated. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="article"></param>


        [HttpPut]
        public async Task<ActionResult> UpdateArticle(Article article)
        {
            if (article == null) return BadRequest(Message("Article can't be null"));
            try
            {   var _currentUser=GetCurrentUser();
                return _articleService.UpdateArticle(article, _currentUser.UserId,_currentUser.IsReviewer) ? await Task.FromResult(Ok(Message("Successfully updated the article"))) : BadRequest(Message("Error Occured while Updating the article"));
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "UpdateArticle(Article article)", exception, article));
                return BadRequest(Message($"{exception.Message}", article));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "UpdateArticle(Article article)", exception, article));
                return Problem($"Error Occured while Adding article :{HelperService.PropertyList(article)}");
            }
        }
        
        /// <summary>
        /// Update a article as private.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Article/UpdatePrivateArticle
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         artileId*: int,
        ///         title*: string,
        ///         content*: string,
        ///         image*: string
        ///         articleStatusID*: int,
        ///         reviewerId*: int,
        ///         datetime*: "2022-06-22T04:00:01.462Z",
        ///         isPrivate*: bool,
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the private article was updated. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="privateArticleDto"></param>
        /// 
        /// 
        [HttpPut]
        public async Task<ActionResult> UpdatePrivateArticle(PrivateArticleDto privateArticleDto)
        {
            if (privateArticleDto ==null || privateArticleDto.article == null) return BadRequest(Message("Article can't be null"));
            try
            {   var _currentUser=GetCurrentUser();
                return _articleService.UpdateArticle(privateArticleDto.article, _currentUser.UserId,_currentUser.IsReviewer,privateArticleDto.SharedUsersId!) ? await Task.FromResult(Ok(Message("Successfully updated the article"))) : BadRequest(Message("Error Occured while Updating the article"));
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "UpdatePrivateArticle(PrivateArticleDto privateArticleDto)", exception, privateArticleDto.article));
                return BadRequest(Message($"{exception.Message}", privateArticleDto.article));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "UpdatePrivateArticle(PrivateArticleDto privateArticleDto)", exception, privateArticleDto.article));
                return Problem($"Error Occured while Adding article :{HelperService.PropertyList(privateArticleDto.article)}");
            }
        }


        /// <summary>
        /// Change Article Status
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Article/ChangeArticleStatus
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         artileId*: int,
        ///         ArticleStatusID*: int,
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the articlestatus was changed. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="ArticleId"></param>
        /// <param name="ArticleStatusID"></param>


        [HttpPatch]
        public async Task<ActionResult> ChangeArticleStatus(int ArticleId, int ArticleStatusID)
        {
            if (ArticleId <= 0 || ArticleStatusID <= 0 || ArticleStatusID > 4) return BadRequest(Message("Article ID  and Article Status ID must be greater than 0 and ArticleStatusID must be less than or equal to 4"));
            if (ArticleStatusID >= 3 && !GetCurrentUser().IsReviewer) return BadRequest(Message("Only Reviewer can able to change the status"));
            try
            {
                return _articleService.ChangeArticleStatus(ArticleId, ArticleStatusID, GetCurrentUser().UserId) ? await Task.FromResult(Ok($"Successfully updated the status of the Article :{ArticleId}")) : BadRequest(Message($"Error Occurred while updating the status of the Article:{ArticleId}"));
            }
            catch (ItemNotFoundException exception) //Occurs When Existing article doesnot match with Article Id
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "ChangeArticleStatus(int ArticleId, int ArticleStatusID)", exception, ArticleId, ArticleStatusID));
                return BadRequest($"{exception.Message}");
            }
            catch (ValidationException exception)  //Occurs When Reviewer updating status for his own articles
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "ChangeArticleStatus(int ArticleId, int ArticleStatusID)", exception, ArticleId, ArticleStatusID));
                return BadRequest(Message($"{exception.Message}"));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "ChangeArticleStatus(int ArticleId, int ArticleStatusID)", exception, ArticleId, ArticleStatusID));
                return Problem($"Error Occurred while updating the status of the Article :{ArticleId}");
            }
        }

        /// <summary>
        /// Delete articles by its id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Article/DeleteArticleByArticleId
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         artileId*: int,
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the article was deleted successfully. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="ArticleId"></param>
        /// <returns></returns>

        [HttpDelete]
        public async Task<ActionResult> DeleteArticleByArticleId(int ArticleId)
        {
            if (ArticleId <= 0) return BadRequest(Message("Article ID must be greater than 0"));
            try
            {
                return _articleService.DeleteArticleByArticleId(ArticleId) ? await Task.FromResult(Ok(Message("Successfully Deleted the draft article"))) : BadRequest(Message($"Error Occured while deleting article with ArticleID:{ArticleId} "));
            }
            catch (ArgumentException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "DeleteArticleByArticleId(int ArticleId)", exception, ArticleId));
                return BadRequest($"{exception.Message}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "DeleteArticleByArticleId(int ArticleId)", exception, ArticleId));
                return Problem($"Error Occurred while Adding like to ArticleId :{ArticleId}");
            }
        }


        /// <summary>
        /// Get Latest Articles.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Article/GetLatestArticles
        ///
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         Range*: int,
        ///     }
        /// </remarks>
        /// <response code="200">Returns a latest articles in a articles. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="Range"></param>
         
        
        [HttpGet]
        public async Task<ActionResult> GetLatestArticles(int Range = 0)
        {
            try
            {
                var Articles = _articleService.GetLatestArticles().ToList();
                return await Task.FromResult(Ok(Articles));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", " GetLatestArticles(int Range)", exception));
                return Problem("Error occured while processing your request");
            }
        }


        /// <summary>
        /// Get Trending Articles.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Article/GetTrendingArticles
        ///
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         Range*: int,
        ///     }
        /// </remarks>
        /// <response code="200">Returns a trending articles in a articles. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="Range"></param>
        
         
        [HttpGet]
        public async Task<ActionResult> GetTrendingArticles(int Range = 0)
        {
            try
            {
                var Articles = _articleService.GetTrendingArticles(Range).ToList();
                return await Task.FromResult(Ok(Articles));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "GetTrendingArticles(int Range)", exception));
                return Problem("Error occured while processing your request");
            }
        }


        /// <summary>
        /// Get a Article by its Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Article/GetArticleById
        ///
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         ArticleId*: int,
        ///     }
        /// </remarks>
        /// <response code="200">Returns a article by its Id . </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="ArticleId"></param>
        
        [HttpGet]
        public async Task<ActionResult> GetArticleById(int ArticleId)
        {
            if (ArticleId <= 0) return BadRequest(Message("Article ID must be greater than 0"));
            try
            {
                return await Task.FromResult(Ok(_articleService.GetArticleById(ArticleId,GetCurrentUser())));
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "GetArticleById(int ArticleId)", exception, ArticleId));
                return BadRequest(Message($"{exception.Message}"));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "GetArticleById(int ArticleId)", exception, ArticleId));
                return Problem($"Error Occurred while getting Article with ArticleId :{ArticleId}");
            }
        }


        /// <summary>
        /// Gets all articles.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Article/GetAll
        ///
        /// </remarks>
        /// <response code="200">Returns all articles. </response>
        /// <response code="500">If there is problem in server. </response>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var Articles = _articleService.GetListOfArticles();    // "ArticleController" is a property of enum class
                return await Task.FromResult(Ok(Articles));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", " GetAll()", exception));
                return Problem("Error occured while processing your request");
            }
        }


        /// <summary>
        /// Gets list of articles by user id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Article/GetArticlesByUserId
        ///
        /// </remarks>
        /// <response code="200">Returns a list of articles by user. </response>
        /// <response code="500">If there is problem in server. </response>
        [HttpGet]
        public async Task<ActionResult> GetArticlesByUserId()
        {
            try
            {
                var ListOfArticleByUserId = _articleService.GetArticlesByUserId(GetCurrentUser().UserId);
                return await Task.FromResult(Ok(ListOfArticleByUserId));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "GetArticlesByUserId(int UserId)", exception));
                return Problem($"Error Occured while processing your request with UserId in Articles :");
            }
        }


        /// <summary>
        /// Gets a list of private articles.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Article/GetPrivateArticles
        /// 
        ///
        /// </remarks>
        /// <response code="200">Returns a list of private articles in that user id</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// 
        [HttpGet]
        public async Task<ActionResult> GetPrivateArticles()
        {
            try
            {
                var ListOfPrivateArticles = _articleService.GetPrivateArticles(GetCurrentUser().UserId);
                return await Task.FromResult(Ok(ListOfPrivateArticles));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", " GetPrivateArticles()", exception));
                return Problem($"Error Occured while processing your request");
            }
        }


        /// <summary>
        /// Get a Article by title
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Article/GetArticlesByTitle
        ///
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         Title*: string,
        ///     }
        /// </remarks>
        /// <response code="200">Returns a article by its title. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="Title"></param>
        
        [HttpGet]
        public async Task<ActionResult> GetArticlesByTitle(string Title)
        {
            if (String.IsNullOrEmpty(Title)) return BadRequest(Message("Title can't be null"));
            try
            {
                var ListOfArticlesByTitle = _articleService.GetArticlesByTitle(Title);
                return await Task.FromResult(Ok(ListOfArticlesByTitle));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "GetArticlesByTitle(string Title)", exception, Title));
                return Problem($"Error Occured while processing your request with Title :{Title}");
            }
        }


       
        /// <summary>
        /// Get a Article by author.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Article/GetArticlesByAuthor
        ///
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         AuthorName*: string,
        ///     }
        /// </remarks>
        /// <response code="200">Returns a article by author name. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="AuthorName"></param>
        /// 
        [HttpGet]
        public async Task<ActionResult> GetArticlesByAuthor(string AuthorName)
        {
            if (String.IsNullOrEmpty(AuthorName)) return BadRequest(Message("Author Name can't be null"));
            try
            {
                var ListOfArticlesByAuthor = _articleService.GetArticlesByTitle(AuthorName);
                return await Task.FromResult(Ok(ListOfArticlesByAuthor));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "GetArticlesByAuthor(string AuthorName)", exception, AuthorName));
                return Problem($"Error Occured while processing your request with Title :{AuthorName}");
            }
        }


       /// <summary>
        /// Get a Article by article status id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/Article/GetArticlesByArticleStatusId
        ///
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         ArticleStatusID*: string,
        ///     }
        /// </remarks>
        /// <response code="200">Returns a article by author name. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="ArticleStatusID"></param>
        

        [HttpGet]
        public async Task<IActionResult> GetArticlesByArticleStatusId(int ArticleStatusID)
        {
            if (ArticleStatusID <= 0 || ArticleStatusID > 4) return BadRequest(Message($"Article Status Id must be between 0 and 4 ArticleStatusID:{ArticleStatusID}"));
            try
            {
                var ListOfArticles = _articleService.GetArticlesByArticleStatusId(ArticleStatusID, GetCurrentUser().IsReviewer);
                return await Task.FromResult(Ok(ListOfArticles));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", "GetArticlesByArticleStatusId(int ArticleStatusID)", exception), ArticleStatusID);
                return Problem("Some Internal Error Occurred");
            }
        }


        /// <summary>
        /// Gets a list of comments by article id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url: https://localhost:7197/Article/GetComments
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         ArticleId*: int,
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns a list of comment by article id.. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="ArticleId"></param>
        [HttpGet]
        public async Task<ActionResult> GetComments(int ArticleId)
        {
            if (ArticleId <= 0) return BadRequest(Message("ArticleId must be greater than 0"));
            try
            {
                var ListOfComments = _articleService.GetComments(ArticleId);
                return await Task.FromResult(Ok(ListOfComments));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleController", nameof(GetComments), exception, ArticleId));
                return Problem($"Error Occured while processing your request with ArticleId :{ArticleId}");
            }
        }
    }
}