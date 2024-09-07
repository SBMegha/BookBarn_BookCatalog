using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarn_DomainLayer.Entities
{
    public class Author
    {
        [Key]
        [Required]
        public int AuthorId { get; set; }

        [Required]
        public string AuthorName { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
