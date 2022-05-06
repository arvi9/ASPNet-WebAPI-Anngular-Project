using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using AspireOverflow.Models;
using AspireOverflow.Services;
using AspireOverflow.CustomExceptions;
using Microsoft.AspNetCore.Authorization;



namespace AspireOverflow.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class ArticleController : ControllerBase
{
    private ILogger<ArticleController> _logger;
    private ArticleService _articleService;

    public ArticleController(ILogger<ArticleController> logger, ArticleService articleService)
    {
        _logger = logger;
        _articleService = articleService;

    }



    [HttpPost]

    public async Task<ActionResult<Article>> CreateArticle(Article article)
    {
        if (article == null) return BadRequest("Null value is not supported");
        try
        {
            return _articleService.CreateArticle(article) ? await Task.FromResult(Ok("Successfully Created")) : BadRequest($"Error Occured while Adding Article :{HelperService.PropertyList(article)}");
        }
        catch (ValidationException exception)
        {
            //HelperService.LoggerMessage - returns string for logger with detailed info
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(CreateArticle), exception, article));
            return BadRequest($"{exception.Message}\n{HelperService.PropertyList(article)}");
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(CreateArticle), exception, article));
            return Problem($"Error Occured while Adding Article :{HelperService.PropertyList(article)}");
        }
    }


    [HttpPost]
    public async Task<ActionResult<ArticleComment>> CreateComment(ArticleComment comment)
    {

        if (comment == null) return BadRequest("Null value is not supported");
        try
        {
            return _articleService.CreateComment(comment) ? await Task.FromResult(Ok("Successfully added comment to the Article")) : BadRequest($"Error Occured while Adding Comment :{HelperService.PropertyList(comment)}");
        }
        catch (ValidationException exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(CreateComment), exception, comment));
            return BadRequest($"{exception.Message}\n{HelperService.PropertyList(comment)}");
        }

        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(CreateComment), exception, comment));
            return Problem($"Error Occured while Adding comment :{HelperService.PropertyList(comment)}");
        }

    }


    [HttpPost]
    public async Task<ActionResult<Article>> AddLikeToArticle(int ArticleId)
    {
        if (ArticleId <= 0) return BadRequest("Article ID must be greater than 0");
        var CurrentUserId = User.FindFirst("userId").Value;
        int UserId = CurrentUserId != null ? Convert.ToInt32(CurrentUserId) : 0;
        try
        {
            return _articleService.AddLikeToArticle(ArticleId, Convert.ToInt32(CurrentUserId)) ? await Task.FromResult(Ok("Successfully added like to article")) : BadRequest("Error Occured while adding like to article ");
        }
        catch (ArgumentException exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(GetArticleById), exception, ArticleId));
            return Problem($"{exception.Message}");
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(GetArticleById), exception, ArticleId));
            return BadRequest($"Error Occurred while Adding like to ArticleId :{ArticleId}");
        }
    }

    [HttpPut]

    public async Task<ActionResult> UpdateArticle(Article article)
    {
        if (article == null) return BadRequest("Article can't be null");
        var CurrentUserId = User.FindFirst("userId").Value;
        if (article.CreatedBy.ToString() != CurrentUserId) BadRequest("Article must be updated only by Article Creater");
        try
        {
            return _articleService.UpdateArticle(article, Convert.ToInt32(CurrentUserId)) ? await Task.FromResult(Ok("Successfully updated the article")) : BadRequest("Error Occured while Updating the article");
        }
        catch (ValidationException exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(UpdateArticle), exception, article));
            return BadRequest($"{exception.Message}\n{HelperService.PropertyList(article)}");
        }

        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(UpdateArticle), exception, article));
            return Problem($"Error Occured while Adding article :{HelperService.PropertyList(article)}");
        }

    }


    [HttpPatch]
    public async Task<ActionResult> ChangeArticleStatus(int ArticleId, int ArticleStatusID)
    {
        if (ArticleId <= 0 && ArticleStatusID <= 0 && ArticleStatusID > 4) return BadRequest("Article ID  and Article Status ID must be greater than 0 and ArticleStatusID must be less than or equal to 4");
         var CurrentUserId = User.FindFirst("userId").Value;
        int UserId = CurrentUserId != null ? Convert.ToInt32(CurrentUserId) : 0;
        try
        {

            return _articleService.ChangeArticleStatus(ArticleId, ArticleStatusID,UserId) ? await Task.FromResult(Ok($"Successfully updated the status of the Article :{ArticleId}")) : BadRequest($"Error Occurred while updating the status of the Article:{ArticleId}");
        }
        catch (ItemNotFoundException exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(ChangeArticleStatus), exception, ArticleId, ArticleStatusID));
            return NotFound($"{exception.Message}");
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(ChangeArticleStatus), exception, ArticleId, ArticleStatusID));
            return Problem($"Error Occurred while updating the status of the Article :{ArticleId}");
        }
    }


    
    [HttpDelete]
    public async Task<ActionResult<Article>> DeleteArticleByArticleId(int ArticleId)
    {
        if (ArticleId <= 0) return BadRequest("Article ID must be greater than 0");
        try
        {
            return _articleService.DeleteArticleByArticleId(ArticleId) ? await Task.FromResult(Ok("Successfully Deleted the draft article")) : BadRequest($"Error Occured while deleting article with ArticleID:{ArticleId} ");
        }
        catch (ArgumentException exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(GetArticleById), exception, ArticleId));
            return Problem($"{exception.Message}");
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(DeleteArticleByArticleId), exception, ArticleId));
            return BadRequest($"Error Occurred while Adding like to ArticleId :{ArticleId}");
        }
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerator<Article>>> GetLatestArticles(int Range)
    {
        try
        {
            var Articles = _articleService.GetLatestArticles().ToList();

            if (Range <= Articles.Count())
            {
                Articles = Range > 0 ? Articles.GetRange(1, Range) : Articles;
                return await Task.FromResult(Ok(Articles));
            }
            else return BadRequest("Range limit exceeded");

        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(GetLatestArticles), exception));
            return Problem("Error occured while processing your request");
        }
    }

    [HttpGet]
    public async Task<ActionResult> GetTrendingArticles(int Range)
    {
        try
        {
            var Articles = _articleService.GetTrendingArticles().ToList();

            if (Range <= Articles.Count())
            {
                Articles = Range > 0 ? Articles.GetRange(1, Range) : Articles;
                return await Task.FromResult(Ok(Articles));
            }
            else return BadRequest("Range limit exceeded");
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(GetTrendingArticles), exception));
            return Problem("Error occured while processing your request");
        }
    }


    [HttpGet]
    public async Task<ActionResult<Article>> GetArticleById(int ArticleId)
    {
        if (ArticleId <= 0) return BadRequest("Article ID must be greater than 0");
        try
        {
            return await Task.FromResult(_articleService.GetArticleById(ArticleId));
        }
        catch (ItemNotFoundException exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(GetArticleById), exception, ArticleId));
            return Problem($"{exception.Message}");
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(GetArticleById), exception, ArticleId));
            return BadRequest($"Error Occurred while getting Article with ArticleId :{ArticleId}");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerator<Article>>> GetAll()
    {
        try
        {

            var Articles = _articleService.GetArticles();    // nameof(ArticleController) is a property of enum class

            return await Task.FromResult(Ok(Articles));
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(GetAll), exception));
            return Problem("Error occured while processing your request");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerator<Article>>> GetArticleByUserId(int UserId)
    {
        if (UserId <= 0) return BadRequest("UserId must be greater than 0");
        try
        {
            var ListOfArticleByUserId = _articleService.GetArticleByUserId(UserId);

            return await Task.FromResult(Ok(ListOfArticleByUserId));
        }

        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(GetArticleByUserId), exception, UserId));
            return Problem($"Error Occured while processing your request with UserId in Articles :{UserId}");
        }

    }


    [HttpGet]
    public async Task<ActionResult<IEnumerator<Article>>> GetArticlesByTitle(string Title)
    {
        if (String.IsNullOrEmpty(Title)) return BadRequest("Title can't be null");
        try
        {
            var ListOfArticlesByTitle = _articleService.GetArticlesByTitle(Title);

            return await Task.FromResult(Ok(ListOfArticlesByTitle));
        }

        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(GetArticlesByTitle), exception, Title));
            return Problem($"Error Occured while processing your request with Title :{Title}");

        }


    }



    [HttpGet]
    public async Task<ActionResult<IEnumerator<Article>>> GetArticlesByAuthor(string AuthorName)
    {
        if (String.IsNullOrEmpty(AuthorName)) return BadRequest("Author Name can't be null");
        try
        {
            var ListOfArticlesByAuthor = _articleService.GetArticlesByTitle(AuthorName);

            return await Task.FromResult(Ok(ListOfArticlesByAuthor));
        }

        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(GetArticlesByAuthor), exception, AuthorName));
            return Problem($"Error Occured while processing your request with Title :{AuthorName}");

        }


    }


    [HttpGet]
    public async Task<ActionResult<IEnumerator<QueryComment>>> GetComments(int ArticleId)
    {
        if (ArticleId <= 0) return BadRequest("ArticleId must be greater than 0");
        try
        {
            var ListOfComments = _articleService.GetComments(ArticleId);

            return await Task.FromResult(Ok(ListOfComments));
        }

        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(GetComments), exception, ArticleId));
            return Problem($"Error Occured while processing your request with ArticleId :{ArticleId}");


        }

    }

}
