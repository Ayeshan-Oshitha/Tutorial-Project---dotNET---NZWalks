using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Data
{
    public class NZWalksDbContext : DbContext
    {


        //Here we should pass Db options, because we later need to send our own connection through the program.cs
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        {
            
        }

        //Dbset is a property of Dbcontext class that represents the collection of entities in the database
        public DbSet<Difficulty> Difficulties  { get; set; }
        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }
    }
}
