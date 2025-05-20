using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BookStoreAppApI.Models.Author
{
    public class AuthorCreateDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }


        [StringLength(250)]
        public string? Bio { get; set; }




    }
}
