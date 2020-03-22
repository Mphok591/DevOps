using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ProductApp.Models;

namespace ProductApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Display(Name ="Name")]
        public string ProductName { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Release Date")]
        public DateTime ReleaseDate { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }

    public class Comment
    {
        public int CommentId { get; set; }

        [Display(Name = "Comment")]
        public string CommentName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Date Of Comment")]
        public DateTime DateOfComment { get; set; }
        public Product Product { get; set; }
    }
}
