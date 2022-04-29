using AspireOverflow.Services;
namespace AspireOverflow.DataAccessLayer
{
    public class QueryRepositoryFactory
    {
        public static QueryRepository GetQueryRepositoryObject(ILogger<QueryService> logger)
        {
            try
            {  
                var aspireOverflowContext = AspireOverflowContextFactory.GetAspireOverflowContextObject();
                 return new QueryRepository(aspireOverflowContext,logger);
            }
            catch (Exception exception)
            {
                logger.LogError($"{exception.Message},{exception.StackTrace}");
                throw exception;
            }
           
        }


    }
}