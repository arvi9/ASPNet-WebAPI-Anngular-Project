

using AspireOverflow.Models;
using Microsoft.EntityFrameworkCore;
namespace AspireOverflow.DataAccessLayer
{
    public class AspireOverflowContext : DbContext
    {

   
        public AspireOverflowContext(DbContextOptions<AspireOverflowContext> options) : base(options)
        { }


       
        public DbSet<Query> Queries { get; set; }
        public DbSet<QueryComment> QueryComments { get; set; }


    }
}