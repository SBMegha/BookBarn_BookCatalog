using BookBarn_API_Layer.DTOs;
using BookBarn_DataLayer.Respositories;
using BookBarn_DomainLayer.Entities;
using BookBarn_DomainLayer.Respositories;

namespace BookBarn_API_Layer.Services
{
    public class BookServices
    {
        private readonly IBookRepository bookRepository = null;
        private readonly IAuthorRepository authorRepository = null;
        private readonly ICategoryRepository categoryRepository = null;
        public BookServices(IBookRepository bookRepository, ICategoryRepository categoryRepository, IAuthorRepository authorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task<List<BookDTO>> GetAllBooksAsync(int pageNumber, int pageSize)
        {
            try
            {
                // Retrieve books with pagination from the repository
                var books = await bookRepository.GetAllBooksAsync(pageNumber, pageSize);

                // Map books to BookDTO
                /*List<BookDTO> bookDTOs = books.Select(book => new BookDTO
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Description = book.Description,
                    Price = book.Price,
                    Image = book.Image,
                    AverageRating = book.AverageRating,
                    AvailableBookCount = book.AvailableBookCount,
                    IsAvailable = book.AvailableBookCount > 0 ? true : false,
                    AuthorId = book.AuthorId,
                    AuthorName = book.Author != null ? book.Author.AuthorName : null,
                    CategoryId = book.CategoryId,
                    CategoryName = book.Category != null ? book.Category.CategoryName : null
                }).ToList();*/

                List<BookDTO> bookDTOs = books.Select(book => MapBookToBookDto(book)).ToList();

                return bookDTOs;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
        }

       /* public async Task<List<BookDTO>> GetAllBooksAsync()
        {
            try
            {
                List<Book> books = await bookRepository.GetAllBooksAsync();

                List<BookDTO> bookDTOs = books.Select(book => new BookDTO
                {
                    Title = book.Title,
                    Description = book.Description,
                    Price = book.Price,
                    Image = book.Image,
                    AverageRating = book.AverageRating,
                    AvailableBookCount = book.AvailableBookCount,
                    IsAvailable= book.AvailableBookCount>0,
                    AuthorId = book.AuthorId,
                    AuthorName = book.Author?.AuthorName ?? string.Empty,
                    CategoryId = book.CategoryId,
                    CategoryName = book.Category?.CategoryName ?? string.Empty
                }).ToList();

                return bookDTOs;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
        }
       */

        public async Task<BookDTO> GetBookByIdAsync(int id)
        {
            try
            {
                Book book = await bookRepository.GetBookByIdAsync(id);

                if (book == null)
                {
                    return null;
                }

                BookDTO bookDTO = MapBookToBookDto(book);

                return bookDTO;
            }
            catch (Exception ex)
            {
                // Log the exception 
                throw;
            }
        }

        public async Task<List<BookDTO>> GetBooksByCategoryAsync(int categoryId, int pageNumber, int pageSize)
        {
            try
            {
                // Retrieve books with pagination from the repository
                List<Book> books = await bookRepository.GetBooksByCategoryAsync(categoryId, pageNumber, pageSize);

                // Map books to BookDTO
                /*List<BookDTO> bookDTOs = books.Select(book => new BookDTO
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Description = book.Description,
                    Price = book.Price,
                    Image = book.Image,
                    AverageRating = book.AverageRating,
                    AvailableBookCount = book.AvailableBookCount,
                    IsAvailable = book.AvailableBookCount > 0 ,
                    AuthorId = book.AuthorId,
                    AuthorName = book.Author?.AuthorName,
                    CategoryId = book.CategoryId,
                    CategoryName = book.Category?.CategoryName
                }).ToList();*/

                List<BookDTO> bookDTOs = books.Select(book => MapBookToBookDto(book)).ToList();

                return bookDTOs;
            }
            catch (Exception ex)
            {
                // Log the exception (Add proper logging here)
                // e.g., _logger.LogError(ex, "An error occurred while getting books by category.");

                throw;
            }
        }

        public async Task<BookDTO> GetBookByTitleAsync(string title)
        {
            try
            {
                // Retrieve a book by title from the repository
                var book = await bookRepository.SearchBookByTitleAsync(title);

                if (book == null)
                {
                    return null;
                }

                // Map Book to BookDTO
                var bookDTO = MapBookToBookDto(book);
                return bookDTO;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
        }

        public async Task<List<BookDTO>> GetBooksByAuthorAsync(int authorId, int pageNumber, int pageSize)
        {
            try
            {
                List<Book> books = await bookRepository.GetBooksByAuthorAsync(authorId,pageNumber,pageSize);

                if (books == null || !books.Any())
                {
                    return new List<BookDTO>();
                }

                /*List<BookDTO> bookDTOs = books.Select(book => new BookDTO
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Description = book.Description,
                    Price = book.Price,
                    Image = book.Image,
                    AverageRating = book.AverageRating,
                    AvailableBookCount = book.AvailableBookCount,
                    IsAvailable = book.AvailableBookCount > 0 ? true : false,
                    AuthorId = book.AuthorId,
                    AuthorName = book.Author?.AuthorName,
                    CategoryId = book.CategoryId,
                    CategoryName = book.Category?.CategoryName
                }).ToList();*/

                List<BookDTO> bookDTOs = books.Select(book => MapBookToBookDto(book)).ToList();

                return bookDTOs;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
        }

        public async Task<BookDTO> AddBookAsync(BookDTO bookDTO)
        {
            try
            {
                Author author = await authorRepository.GetAuthorByNameAsync(bookDTO.AuthorName);
                if (author == null)
                {
                    author= new Author { AuthorName = bookDTO.AuthorName };
                    author = await authorRepository.AddAsync(author);
                }

                var category = await categoryRepository.GetCategoryByNameAsync(bookDTO.CategoryName);
                if (category == null)
                {
                    category = new Category { CategoryName = bookDTO.CategoryName };
                    category = await categoryRepository.AddAsync(category);
                }

                // Convert BookDTO to Book entity
                Book book = new Book
                {
                    Title = bookDTO.Title,
                    Description = bookDTO.Description,
                    Price = bookDTO.Price,
                    Image = bookDTO.Image,
                    AverageRating = bookDTO.AverageRating,
                    AvailableBookCount = bookDTO.AvailableBookCount,
                    AuthorId = author.AuthorId,
                    CategoryId = category.CategoryId,
                    Author =author,
                    Category = category,
                };

                // Add the book to the repository
                Book addedBook = await bookRepository.AddAsync(book);
                if (addedBook != null)
                {
                    BookDTO bookDto = MapBookToBookDto(addedBook);
                    return bookDto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (Add proper logging here)
                throw;
            }
        }

        public async Task<bool> DeleteBookById(int id)
        {
            try
            {
                if(await bookRepository.DeleteAsync(id))
                     return true; 
                return false;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
        }

        public async Task<BookDTO> UpdateBook(BookDTO bookDTO)
        {
            try
            {
                if (!bookDTO.BookId.HasValue)
                    throw new ArgumentException("Book Id cannot be empty while updating");

                 var existingBook = await bookRepository.GetBookByIdAsync(bookDTO.BookId.Value);
                if (existingBook == null)
                    throw new KeyNotFoundException("Book not found");

                var author = await authorRepository.GetAuthorByNameAsync(bookDTO.AuthorName);
                if (author == null)
                {
                    author = new Author { AuthorName = bookDTO.AuthorName };
                    await authorRepository.AddAsync(author);
                }

                var category = await categoryRepository.GetCategoryByNameAsync(bookDTO.CategoryName);
                if (category == null)
                {
                    category = new Category { CategoryName = bookDTO.CategoryName };
                    await categoryRepository.AddAsync(category);
                }
                // Convert BookDTO to Book entity
               

                // Update the book properties
                existingBook.Title = bookDTO.Title;
                existingBook.Description = bookDTO.Description;
                existingBook.Price = bookDTO.Price;
                existingBook.Image = bookDTO.Image;
                existingBook.AverageRating = bookDTO.AverageRating;
                existingBook.AvailableBookCount = bookDTO.AvailableBookCount;
                existingBook.AuthorId = author.AuthorId;
                existingBook.CategoryId = category.CategoryId;

                // Call the repository to update the book
                Book updatedBook =  await bookRepository.UpdateAsync(existingBook);
                if (updatedBook != null)
                {
                    BookDTO updatedBookDto = MapBookToBookDto(updatedBook);
                    return updatedBookDto;
                }
                else return null;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
        }

        public async Task<int> GetTotalNumberOfBooks()
        {
            return await bookRepository.GetTotalNumberOfBooksAsync();
        }

        public async Task<int> GetTotalNumberOfBooksByCategoryId(int id)
        {
            return await bookRepository.GetTotalNumberOfBooksByCategoryIdAsync(id);
        }
        public async Task<int> GetTotalNumberOfBooksByAuthorId(int id)
        {
            return await bookRepository.GetTotalNumberOfBooksByAuthorIdAsync(id);
        }

        

        /*  public Book MapBookDtoToBook(BookDTO bookDTO) 
          {

          }*/

        public BookDTO MapBookToBookDto(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            BookDTO bookDto = new BookDTO
            {
                BookId = book.BookId,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
                Image = book.Image,
                AverageRating = book.AverageRating,
                AvailableBookCount = book.AvailableBookCount,
                AuthorId = book.AuthorId,
                AuthorName = book.Author?.AuthorName, // Handle possible null Author
                CategoryId = book.CategoryId,
                CategoryName = book.Category?.CategoryName // Handle possible null Category

            };
            return bookDto;
        }
    }
}
