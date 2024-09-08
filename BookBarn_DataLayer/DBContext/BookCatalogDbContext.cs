﻿using System;
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, AuthorName = "Author One" },
                new Author { AuthorId = 2, AuthorName = "Author Two" }
            );

            // Seed data for Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Category One" },
                new Category { CategoryId = 2, CategoryName = "Category Two" }
            );

            // Seed data for Books
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 1,
                    Title = "Book One",
                    Description = "Description for Book One",
                    Price = 100,
                    Image = Convert.FromBase64String("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"),
                    AverageRating = 4.5,
                    AvailableBookCount = 10,
                    AuthorId = 1,
                    CategoryId = 1
                },
                new Book
                {
                    BookId = 2,
                    Title = "Book Two",
                    Description = "Description for Book Two",
                    Price = 200,
                    Image = Convert.FromBase64String("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"),
                    AverageRating = 4.7,
                    AvailableBookCount = 5,
                    AuthorId = 2,
                    CategoryId = 2
                }
            );
        }

    }
}
