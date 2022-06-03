
using AspireOverflow.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace  AspireOverflowTest.DataAccessLayer
{

public static class MockDBContext
{
    public static AspireOverflowContext GetInMemoryDbContext(){

        var Options= new DbContextOptionsBuilder<AspireOverflowContext>().UseInMemoryDatabase(databaseName: "AspireOverflow_InMemoryDatabase").Options;
        return new AspireOverflowContext(Options);
    

    }    
}
}
