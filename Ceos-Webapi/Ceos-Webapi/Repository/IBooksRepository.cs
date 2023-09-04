using CEOS_WebApi.Model;

namespace CEOS_WebApi.Repository
{
    public interface IBooksRepository
    {
        /// <summary>
        /// Returns list of books from currently set source
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> GetBooks();

        /// <summary>
        /// Generate book with random properties
        /// </summary>
        /// <returns></returns>
        public Book DoFakeBook();
    }
}