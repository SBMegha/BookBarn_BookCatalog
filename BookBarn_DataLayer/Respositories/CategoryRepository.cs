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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookCatalogDbContext db=null;
        public CategoryRepository(BookCatalogDbContext db)
        {
            this.db = db;
        }

        /*
        public bool Add(Category category)
        {
            try
            {
                db.Categories.Add(category);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                
                //Write logging here later
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                Category category = db.Categories.Find(id);
                if (category != null)
                {
                    db.Categories.Remove(category);
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
        public List<Category> GetAllCategories()
        {
            try
            {
                return db.Categories.ToList();
            }
            catch (Exception ex)
            {
                // log.Error(ex.Message);
                return new List<Category>();
            }
        }
        public Category GetCategory(int id)
        {
            try
            {
                return db.Categories.Find(id);
            }
            catch (Exception ex)
            {
                // log.Error(ex.Message);
                return null;
            }
        }
        public bool Update(Category category)
        {
            try
            {
                Category existingCategory = db.Categories.Find(category.CategoryId);
                if (existingCategory != null)
                {
                    existingCategory.CategoryName = category.CategoryName;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // log.Error(ex.Message);
                return false;
            }
        }
    */


        public async Task<Category> AddAsync(Category category)
        {
            try
            {
                await db.Categories.AddAsync(category);
                await db.SaveChangesAsync();
                return category;
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
                Category category =await db.Categories.FindAsync(id);
                if (category != null)
                {
                    db.Categories.Remove(category);
                    return await db.SaveChangesAsync() > 0;
                }
                else return false;
            }
            catch (Exception ex)
            {
                //Write logging here later
                return false;
            }
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            try
            {
                return await db.Categories.ToListAsync();
            }
            catch (Exception ex)
            {
                // log.Error(ex.Message);
                throw;
            }
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            try
            {
                return await db.Categories.FindAsync(id);
            }
            catch (Exception ex)
            {
                // log.Error(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            try
            {
                Category existingCategory = await db.Categories.FindAsync(category.CategoryId);
                if (existingCategory != null)
                {
                    existingCategory.CategoryName = category.CategoryName;
                    await db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // log.Error(ex.Message);
                return false;
            }
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            try
            {
                Category category = await db.Categories.Where(c => c.CategoryName.ToLower() == name.ToLower()).FirstOrDefaultAsync();
                return category;
            }
            catch (Exception ex)
            {
                // log.Error(ex.Message);
                throw;
            }
        }

    }
}
