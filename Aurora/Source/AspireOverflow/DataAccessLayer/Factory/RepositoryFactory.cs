using AspireOverflow.Services;
namespace AspireOverflow.DataAccessLayer
{
    public class QueryRepositoryFactory
    {
        // public static QueryRepository GetQueryRepositoryObject(ILogger<QueryService> logger)
        // {
        //     try
        //     {  
        //         var aspireOverflowContext = AspireOverflowContextFactory.GetAspireOverflowContextObject();
        //          return new QueryRepository(aspireOverflowContext,logger);
        //     }
        //     catch (Exception exception)
        //     {
        //         logger.LogError($"{exception.Message},{exception.StackTrace}");
        //         throw exception;
        //     }
           
        // }


    }
     public class ArticleRepositoryFactory
    {
        public static ArticleRepository GetArticleRepositoryObject(ILogger<ArticleService> logger)
        {
            try
            {  
                var aspireOverflowContext = AspireOverflowContextFactory.GetAspireOverflowContextObject();
                 return new ArticleRepository(aspireOverflowContext,logger);
            }
            catch (Exception exception)
            {
                logger.LogError($"{exception.Message},{exception.StackTrace}");
                throw exception;
            }
           
        }


    }

     public class UserRepositoryFactory
    {
        public static UserRepository GetUserRepositoryObject(ILogger<UserService> logger)
        {
            try
            {  
                var aspireOverflowContext = AspireOverflowContextFactory.GetAspireOverflowContextObject();
                 return new UserRepository(aspireOverflowContext,logger);
            }
            catch (Exception exception)
            {
                logger.LogError($"{exception.Message},{exception.StackTrace}");
                throw exception;
            }
           
        }


    }
}