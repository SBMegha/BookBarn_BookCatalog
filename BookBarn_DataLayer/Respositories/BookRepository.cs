using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarn_DataLayer.DBContext;
using BookBarn_DomainLayer.Entities;
using BookBarn_DomainLayer.Respositories;
using Microsoft.EntityFrameworkCore;

namespace BookBarn_DataLayer.Respositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookCatalogDbContext db = null;
        public BookRepository(BookCatalogDbContext db)
        {
            this.db = db;
        }

        public bool Add(Book book)
        {
            try
            {
                var existingAuthor = db.Authors.FirstOrDefault(a => a.AuthorName == book.Author.AuthorName);
                if (existingAuthor != null)
                {
                    book.AuthorId = existingAuthor.AuthorId;
                }
                else
                {
                    var newAuthor = new Author { AuthorName = book.Author.AuthorName };
                    db.Authors.Add(newAuthor);
                    db.SaveChanges();
                    book.AuthorId = newAuthor.AuthorId;
                }

                db.Books.Add(book);

                // Save changes
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                Book book = GetBookById(id);
                if (book != null)
                {
                    db.Books.Remove(book);
                    return db.SaveChanges() > 0;
                }
                else return false;
            }
            catch (Exception ex)
            {
                //Write logging here later
                return false;
            }
        }
        public bool Update(Book book)
        {
            try
            {
                Book existingBook = db.Books.Find(book.BookId);
                if (existingBook != null)
                {
                    existingBook.Title = book.Title;
                    existingBook.AuthorId = book.AuthorId;
                    existingBook.Description = book.Description;
                    existingBook.Price = book.Price;
                    existingBook.AvailableBookCount = book.AvailableBookCount;
                    existingBook.Image = book.Image;
                    existingBook.AverageRating = book.AverageRating;
                    existingBook.CategoryId = book.CategoryId;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public List<Book> GetAllBooks()
        {
            try
            {
                return db.Books.ToList();
            }
            catch (Exception ex)
            {
                // log.Error(ex.Message);
                return new List<Book>();
            }
        }
        public Book GetBookById(int id)
        {
            try
            {
                return db.Books
                            .Include(b => b.Author)  // Eager load Author
                            .Include(b => b.Category) // Eager load Category
                            .FirstOrDefault(b => b.BookId == id);
            }
            catch (Exception ex)
            {
                // log.Error(ex.Message);
                return null;
            }
        }
        public Book SearchBookByTitle(string title)
        {
            try
            {
                return db.Books.FirstOrDefault(b => b.Title == title);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public List<Book> GetBooksByCategory(int categoryId)
        {
            try
            {
                return db.Books.Where(b => b.CategoryId == categoryId).ToList();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return new List<Book>();
            }
        }
        public List<Book> GetBooksByAuthor(int authorId)
        {
            try
            {
                return db.Books.Where(b => b.AuthorId == authorId).ToList();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return new List<Book>();
            }
        }
        public List<Book> GetAllBooks(int pageNumber, int pageSize)
        {
            try
            {
                return db.Books
                    .Include(b => b.Author)
                    .Include(b => b.Category)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            catch (Exception ex)
            {
                // Log the exception
                return new List<Book>();
            }
        }
        public List<Book> GetBooksByAuthor(int authorId, int pageNumber, int pageSize)
        {
            try
            {
                return db.Books
                    .Where(b => b.AuthorId == authorId)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return new List<Book>();
            }
        }
        public List<Book> GetBooksByCategory(int categoryId, int pageNumber, int pageSize)
        {
            try
            {
                return db.Books
                    .Where(b => b.CategoryId == categoryId)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return new List<Book>();
            }
        }
        


        //Async
        public async Task<Book> AddAsync(Book book)
        {
            try
            {
                await db.Books.AddAsync(book);
                var result = await db.SaveChangesAsync();
                return result > 0 ? book : null;
            }
            catch (Exception ex)
            {
                //Write logging here later
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Book book = await GetBookByIdAsync(id);
                if (book != null)
                {
                    db.Books.Remove(book);
                    return await db.SaveChangesAsync() > 0;
                }
                else return false;
            }
            catch (Exception ex)
            {
                //Write logging here later
                throw new Exception(ex.Message);
            }
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            try
            {
                Book existingBook = await GetBookByIdAsync(book.BookId);
                if (existingBook != null)
                {
                    existingBook.Title = book.Title;
                    existingBook.AuthorId = book.AuthorId;
                    existingBook.Description = book.Description;
                    existingBook.Price = book.Price;
                    existingBook.AvailableBookCount = book.AvailableBookCount;
                    existingBook.Image = book.Image;
                    existingBook.AverageRating = book.AverageRating;
                    existingBook.CategoryId = book.CategoryId;
                    await db.SaveChangesAsync();
                    return book;
                }
                return null;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            try
            {
                return await db.Books
                        .Include(b => b.Author)
                        .Include(b => b.Category)
                        .ToListAsync();
                
            }
            catch (Exception ex)
            {
                // log.Error(ex.Message);
                return new List<Book>();
            }
        }

        public async Task<Book> SearchBookByTitleAsync(string title)
        {
            try
            {
                return await db.Books.FirstOrDefaultAsync(b => b.Title == title);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            try
            {
                return await db.Books
                         .Include(b => b.Author)  // Eager load Author
                         .Include(b => b.Category) // Eager load Category
                         .FirstOrDefaultAsync(b => b.BookId == id);
            }
            catch (Exception ex)
            {
                // log.Error(ex.Message);
                throw;
            }
        }

        public async Task<List<Book>> GetBooksByCategoryAsync(int categoryId)
        {
            try
            {
                return await db.Books.Where(b => b.CategoryId == categoryId).ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return new List<Book>();
            }
        }

        public async Task<List<Book>> GetBooksByAuthorAsync(int authorId)
        {
            try
            {
                return await db.Books.Where(b => b.AuthorId == authorId).ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return new List<Book>();
            }
        }

        public async Task<int> GetTotalNumberOfBooksAsync()
        {
            return await db.Books.CountAsync();
        }


        //Pagination methods
        public async Task<List<Book>> GetAllBooksAsync(int pageNumber, int pageSize)
        {
            try
            {
                return await db.Books
                         .Include(b => b.Author)  // Eager load Author
                         .Include(b => b.Category) // Eager load Category
                         .Skip((pageNumber - 1) * pageSize) // Pagination
                         .Take(pageSize) // Limit the number of results
                         .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                return new List<Book>();
            }
        }

        public async Task<List<Book>> GetBooksByAuthorAsync(int authorId, int pageNumber, int pageSize)
        {
            try
            {
                return await db.Books
                    .Where(b => b.AuthorId == authorId)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return new List<Book>();
            }
        }

        public async Task<List<Book>> GetBooksByCategoryAsync(int categoryId, int pageNumber, int pageSize)
        {
            try
            {
                return await db.Books
                    .Where(b => b.CategoryId == categoryId)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return new List<Book>();
            }
        }

        public async Task<int> GetTotalNumberOfBooksByCategoryIdAsync(int id)
        {
            return await db.Books.Where(b => b.CategoryId == id).CountAsync();
        }

        public async Task<int> GetTotalNumberOfBooksByAuthorIdAsync(int id)
        {
            return await db.Books.Where(b => b.AuthorId == id).CountAsync();
        }
    }
}
