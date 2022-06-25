
using AspireOverflow.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace  AspireOverflowTest.DataAccessLayer
{

public static class MockDBContext
{
    public static AspireOverflowContext GetInMemoryDbContext(){

        var Options= new DbContextOptionsBuilder<AspireOverflowContext>().UseInMemoryDatabase(databaseName: "AspireOverflow_InMemoryDatabase").Options;
        return new AspireOverflowContext(Options);
    

    }       public static void SeedMockDataInMemoryDb(AspireOverflowContext dbContext)
        {
           dbContext.Users.AddRange(QueryMock.GetListOfUsersForSeeding());
           dbContext.Queries.AddRange(QueryMock.GetListOfQueriesForSeeding());
            dbContext.SaveChanges();
        }
}
}
