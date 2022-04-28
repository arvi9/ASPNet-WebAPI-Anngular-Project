using AspireOverflow.Controllers;
namespace AspireOverflow.DataAccessLayer
{
    public class QueryRepositoryFactory
    {
        public static IQueryRepository GetQueryRepositoryObject()
        {

            return new QueryRepository(DbContextFactory.GetAspireOverflowContextObject());
        }

        
    }
}