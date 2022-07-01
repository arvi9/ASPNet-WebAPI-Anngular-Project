using System.Linq;
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
    

    }     
             public static void SeedMockDataInMemoryDb(AspireOverflowContext dbContext)
        {
            
           dbContext.Users.AddRange(ArticleMock.GetListOfUsersForSeeding());
           dbContext.Articles.AddRange(ArticleMock.GetListOfArticleForSeeding());
           dbContext.PrivateArticles.AddRange(ArticleMock.GetListOfPrivateArticleForSeeding());
           dbContext.ArticleComments.AddRange(ArticleMock.GetCommentsForSeeding());
           dbContext.Departments.AddRange(UserMock.GetListOfDepartmentsForSeeding());
           dbContext.Designations.AddRange(UserMock.GetListOfDesignationForSeeding());
           dbContext.Genders.AddRange(UserMock.GetListOfGenderForSeeding());
           dbContext.Queries.AddRange(QueryMock.GetListOfQueriesForSeeding());
           dbContext.QueryComments.AddRange(QueryMock.GetListOfCommentsForSeeding());
            dbContext.ArticleLikes.AddRange(ArticleMock.GetListOfArticleLikesForSeeding());
           dbContext.SaveChanges();
        }
}
}