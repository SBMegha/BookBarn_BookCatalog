using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarn_DomainLayer.Entities;

namespace BookBarn_DomainLayer.Respositories
{
    public interface ICategoryRepository
    {
        // Synchronous operations
        /*bool Add(Category category);
        bool Update(Category category);
        bool Delete(int id);
        List<Category> GetAllCategories();
        Category GetCategory(int id);*/

        // Asynchronous operations
        Task<Category> AddAsync(Category category);
        Task<bool> UpdateAsync(Category category);
        Task<bool> DeleteAsync(int id);
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryAsync(int id);

        Task<Category> GetCategoryByNameAsync(string name);
    }
}
