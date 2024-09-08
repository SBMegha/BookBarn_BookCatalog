using BookBarn_DomainLayer.Entities;
using BookBarn_DomainLayer.Respositories;

namespace BookBarn_API_Layer.Services
{
    public class CategoryServices
    {
        private readonly IBookRepository bookRepository = null;
        private readonly IAuthorRepository authorRepository = null;
        private readonly ICategoryRepository categoryRepository = null;
        public CategoryServices(IBookRepository bookRepository, ICategoryRepository categoryRepository, IAuthorRepository authorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await categoryRepository.GetAllCategoriesAsync();
        }
    }
}
