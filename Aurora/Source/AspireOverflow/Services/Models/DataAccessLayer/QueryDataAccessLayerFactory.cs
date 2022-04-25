using AspireOverflow.Controllers;
namespace AspireOverflow.Models
{
    public class QueryDataAccessLayerFactory
    {
        public static IQueryDataAccessLayer GetQueryDataAccessLayerObject()
        {

            return new QueryDataAccessLayer(DbContextFactory.GetAspireOverflowContextObject());
        }

        
    }
}