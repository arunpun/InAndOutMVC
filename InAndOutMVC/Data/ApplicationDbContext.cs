using InAndOutMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOutMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        //Constructor, pass DbContextoptions from ApplicationDbContext to the base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //Create an entity, dbset of Item to hold items
        public DbSet<Item> Items { get; set; }
        public DbSet<Expense> Expenses { get; set; }

    }
}
