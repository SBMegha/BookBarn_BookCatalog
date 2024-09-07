using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarn_DomainLayer.Entities;

namespace BookBarn_DomainLayer.Respositories
{
    public interface IAuthorRepository
    {
        /*bool Add(Author author);
        bool Update(Author author);
        bool Delete(int id);
        List<Author> GetAllAuthors();
        Author GetAuthor(int id);*/

        // Asynchronous methods
        Task<Author> AddAsync(Author author);
        Task<bool> UpdateAsync(Author author);
        Task<bool> DeleteAsync(int id);
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorAsync(int id);

        Task<Author> GetAuthorByNameAsync(string name);
    }
}
