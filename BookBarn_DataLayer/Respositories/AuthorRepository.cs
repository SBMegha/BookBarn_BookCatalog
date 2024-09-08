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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookCatalogDbContext db;
        public AuthorRepository(BookCatalogDbContext db) 
        {
            this.db = db;
        }
        /*public bool Add(Author author)
        {
            try
            {
                db.Authors.Add(author);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                // log.Error(ex.Message);
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var author = db.Authors.Find(id);
                if (author != null)
                {
                    db.Authors.Remove(author);
                    return db.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                // log.Error(ex.Message);
                return false;
            }
        }
        public List<Author> GetAllAuthors()
        {
            try
            {
                return db.Authors.ToList();
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                // log.Error(ex.Message);
                return new List<Author>();
            }
        }
        public Author GetAuthor(int id)
        {
            throw new NotImplementedException();
        }
        public bool Update(Author author)
        {
            try
            {
                var existingAuthor = db.Authors.Find(author.AuthorId);
                if (existingAuthor != null)
                {
                    existingAuthor.AuthorName = author.AuthorName;
                    db.Entry(existingAuthor).State = EntityState.Modified;
                    return db.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                // log.Error(ex.Message);
                return false;
            }
        }*/

        //Async

        public async Task<Author> AddAsync(Author author)
        {
            try
            {
                await db.Authors.AddAsync(author);
                var result = await db.SaveChangesAsync() > 0;
                return author;
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var author = await db.Authors.FindAsync(id);
                if (author != null)
                {
                    db.Authors.Remove(author);
                    return await db.SaveChangesAsync() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                // log.Error(ex.Message);
                return false;
            }
        }
        
        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            try
            {
                return await db.Authors.ToListAsync();
            }
            catch (Exception ex)
            {
                //log
                throw;
            }
        }

        public async Task<Author> GetAuthorAsync(int id)
        {
            try
            {
                return await db.Authors.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
        }      

        public async Task<bool> UpdateAsync(Author author)
        {
            try
            {
                var existingAuthor = await db.Authors.FindAsync(author.AuthorId);
                if (existingAuthor != null)
                {
                    existingAuthor.AuthorName = author.AuthorName;
                    db.Entry(existingAuthor).State = EntityState.Modified;
                    return await db.SaveChangesAsync() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                // log.Error(ex.Message);
                return false;
            }
        }

        public async Task<Author> GetAuthorByNameAsync(string name)
        {
            try
            {
                Author author = await db.Authors.Where(a => a.AuthorName.ToLower() == name.ToLower()).FirstOrDefaultAsync();
                return author;
            }
            catch (Exception ex)
            {
                // log.Error(ex.Message);
                throw;
            }
        }
    }
}
