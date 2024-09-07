using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarn_DomainLayer.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MinLength(5)]
        public string CategoryName { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}
