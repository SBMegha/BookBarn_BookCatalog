using BookBarn_DomainLayer.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookBarn_API_Layer.DTOs
{
    public class BookDTO
    {
        public int? BookId { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Description must be at least 5 characters long.")]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        [Required]
        public string Image { get; set; }
       // public byte[] Image { get; set; }
       

        [Required]
        [Range(0, 5, ErrorMessage = "Average rating must be between 0 and 5.")]
        public double AverageRating { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Available book count must be a non-negative integer.")]
        public int AvailableBookCount { get; set; }

        public bool IsAvailable { get; set; }

        public int AuthorId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Author name is required.")]
        public string AuthorName { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Category name is required.")]
        public string CategoryName { get; set; }

    }
}
