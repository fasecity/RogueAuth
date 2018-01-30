using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RogueIdentity.ApiDbContext
{
    public class ApplicationDbContext : IdentityDbContext
    {

        //ctor
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        // public DbSet<User> Users { get; set; }


        //overides
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //set src sqlite local--works All remember check  appsettings json file
            optionsBuilder.UseSqlite("Data Source=RogueDb.db");


        }


    }
}
