using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarn_DomainLayer.Entities;

namespace BookBarn_DomainLayer.Respositories
{
    public interface IBookRepository
    {
        // Synchronous methods
        bool Add(Book book);
        bool Update(Book book);
        bool Delete(int id);
        Book GetBookById(int id);
        List<Book> GetAllBooks();
        List<Book> GetBooksByCategory(int categoryId);
        List<Book> GetBooksByAuthor(int authorId);
        Book SearchBookByTitle(string title);

        // Asynchronous methods
        Task<Book> AddAsync(Book book);
        Task<Book> UpdateAsync(Book book);
        Task<bool> DeleteAsync(int id);
        Task<Book> GetBookByIdAsync(int id);
        Task<List<Book>> GetAllBooksAsync();
        Task<List<Book>> GetBooksByCategoryAsync(int categoryId);
        Task<List<Book>> GetBooksByAuthorAsync(int authorId);
        Task<Book> SearchBookByTitleAsync(string title);

        //pagination methods
        public List<Book> GetAllBooks(int pageNumber, int pageSize);
        public Task<List<Book>> GetAllBooksAsync(int pageNumber, int pageSize);
        public List<Book> GetBooksByAuthor(int authorId, int pageNumber, int pageSize);
        public Task<List<Book>> GetBooksByAuthorAsync(int authorId, int pageNumber, int pageSize);
        public List<Book> GetBooksByCategory(int categoryId, int pageNumber, int pageSize);
        public Task<List<Book>> GetBooksByCategoryAsync(int categoryId, int pageNumber, int pageSize);
        public Task<int> GetTotalNumberOfBooksAsync();
        public Task<int> GetTotalNumberOfBooksByCategoryIdAsync(int id);
        public Task<int> GetTotalNumberOfBooksByAuthorIdAsync(int id);

    }
}
