using Microsoft.EntityFrameworkCore;

namespace Bank_Accounts.Models
{
      public class MyContext:DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<ForLandR> ForLandRes {get;set;}
        public DbSet<Transaction> Transactiones {get;set;}
        
    }
}