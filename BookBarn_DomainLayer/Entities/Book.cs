using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarn_DomainLayer.Entities
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [MinLength(1)]
        public string Title { get; set; }

        [Required]
        [MinLength(5)]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public byte[] Image { get; set; }
        //public string Image { get; set; }

        [Range(0, 5)]
        public double AverageRating { get; set; }

        [Range(0, int.MaxValue)]
        public int AvailableBookCount { get; set; }

        [ForeignKey(nameof(Author))]
        [Required]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        [ForeignKey(nameof(Category))]
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
