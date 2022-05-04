using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using AspireOverflow.Models;
using AspireOverflow.Services;



namespace AspireOverflow.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ArticleController : ControllerBase
{
    internal static ILogger<ArticleController> _logger;
    private static ArticleService _articleService;

     public ArticleController(ILogger<ArticleController> logger, ArticleService articleService)
    {
        _logger = logger ?? throw new NullReferenceException(nameof(logger));
        _articleService = articleService ?? throw new NullReferenceException(nameof(articleService));

    }

    [HttpPost]
    public async Task<ActionResult<ArticleComment>> CreateComment(ArticleComment comment)
    {

        if (comment == null) return BadRequest("Null value is not supported");
        try
        {
            return _articleService.CreateComment(comment, DevelopmentTeam.Web) ? await Task.FromResult(Ok("Successfully added comment to the Article")) : BadRequest($"Error Occured while Adding Comment :{HelperService.PropertyList(comment)}");
        }  catch (ValidationException exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(CreateComment), exception, comment));
            return BadRequest($"{exception.Message}\n{HelperService.PropertyList(comment)}");
        }

        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(CreateComment), exception, comment));
            return BadRequest($"Error Occured while Adding comment :{HelperService.PropertyList(comment)}");
        }

    }

}
