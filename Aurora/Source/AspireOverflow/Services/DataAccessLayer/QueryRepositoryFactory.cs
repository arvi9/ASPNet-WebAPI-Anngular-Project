using AspireOverflow.Controllers;
namespace AspireOverflow.DataAccessLayer
{
    public class QueryRepositoryFactory
    {
        public static QueryRepository GetQueryRepositoryObject()
        {

            return new QueryRepository(DbContextFactory.GetAspireOverflowContextObject());
        }

        
    }
}