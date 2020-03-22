using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductApp.Models;

namespace ProductApp.Models
{
    public class ProductDB : DbContext
    {
        public ProductDB(DbContextOptions<ProductDB> options)
            : base(options)
        {

        }
        public DbSet<Product> products { get; set; }
        public DbSet<Comment> comments { get; set; }
    }
}
