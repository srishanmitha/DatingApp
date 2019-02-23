using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
namespace DatingApp.API.models.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions< DataContext >options) : base (options) {}
        

         public  DbSet<Value> Values { get;set; }
         public  DbSet<User> Users  { get; set; }
    }

}
    
