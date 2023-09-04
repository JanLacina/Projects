using CEOS_WebApi.Model;

namespace CEOS_WebApi.Repository
{
    public class BooksRepository : IBooksRepository
    {
        /// <summary>
        /// Returns list of books from currently set source
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> GetBooks()
        {
            IList<Book> books = new List<Book>();

            for (int i = 0; i < 15; i++)
            {
                books.Add(DoFakeBook());
            }

            return books;
        }

        /// <summary>
        /// Generate book with random properties
        /// </summary>
        /// <returns></returns>
        public Book DoFakeBook()
        {
            var words = new List<string> { "Test", "Car", "Phone", "of", "Coffee", "Glass", "Speaker", "Crypt", "Ceos", "Task", "Book", "Best"};
            var genres = new List<string> { "Thriller", "Horror", "Romance", "Western", "Historical", "Fiction", "Fantasy"};
            var anotation = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Nulla quis diam. Aliquam ante. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Curabitur ligula sapien, pulvinar a vestibulum quis, facilisis vel sapien. Fusce aliquam vestibulum ipsum. Praesent dapibus. Nunc tincidunt ante vitae massa. Sed vel lectus. Donec odio tempus molestie, porttitor ut, iaculis quis, sem. Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur? Vivamus luctus egestas leo. Proin mattis lacinia justo. Phasellus enim erat, vestibulum vel, aliquam a, posuere eu, velit. Fusce suscipit libero eget elit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos hymenaeos. Nulla non lectus sed nisl molestie malesuada.";
            var firstNames = new List<string> { "John", "Paul", "George", "Ringo", "Jenna", "Crystal", "Sunny", "Mia"};
            var lastNames = new List<string> { "Smith", "Brown", "Black", "White", "McDonald", "Notebook", "Tralala", "Ferrari"};

            Book book = new Book();
            string bookTitle = string.Empty;

            Random random = new Random();

            bool.TryParse($"{random.Next(0, 1)}", out bool randomBool);
            int numberOfWords = random.Next(0,5);

            book.Anotation = anotation;
            book.AuthorFirstName = firstNames[random.Next(0,firstNames.Count)];
            book.AuthorMiddleName = randomBool ? firstNames[random.Next(0,firstNames.Count)] : "";
            book.AuthorLastName = lastNames[random.Next(0,lastNames.Count)];
            book.Genre = genres[random.Next(0, genres.Count)];
            book.PublicationYear = random.Next(1900, 2023);

            bookTitle += randomBool ? "The " : "";
            book.ISBN = $"978-{random.Next(0, 99)}-{random.Next(0, 999)}-{random.Next(0, 99999)}-0";

            for (int i = 0; i < numberOfWords; i++)
            {
                for (int j = 0; j < numberOfWords; j++)
                {
                    bookTitle += words[random.Next(0, words.Count)];

                    if (!j.Equals(numberOfWords - 1))
                    {
                        bookTitle += " ";
                    }
                }
            }

            return book;
        }
    }
}
