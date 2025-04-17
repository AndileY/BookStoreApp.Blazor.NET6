using BookStoreAppApI.Models.Book;

namespace BookStoreAppApI.Models.Author
{
    public class AuthorDetailsDto : AuthorReadOnyDto
    {
        public List<BookReadOnlyDto> Books { get; set; }
    }
}
