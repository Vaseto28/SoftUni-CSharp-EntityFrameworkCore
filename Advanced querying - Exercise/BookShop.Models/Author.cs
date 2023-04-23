namespace BookShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BookShop.Models.Validatons;

    public class Author
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
        }

        [Key]
        public int AuthorId { get; set; }

        [MaxLength(ValidationConstraints.AuthorFirstNameMaxLength)]
        public string? FirstName { get; set; }

        [MaxLength(ValidationConstraints.AuthorLastNameMaxLength)]
        public string LastName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}