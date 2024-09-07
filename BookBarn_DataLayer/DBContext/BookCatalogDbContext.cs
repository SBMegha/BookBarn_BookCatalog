using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarn_DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookBarn_DataLayer.DBContext
{
    public class BookCatalogDbContext : DbContext
    {
        public DbSet<Book> Books {  get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }

        public BookCatalogDbContext() { }

        public BookCatalogDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=BookBarnCatalog;Integrated Security=True;Encrypt=True ");
            }
        }

    }
}
