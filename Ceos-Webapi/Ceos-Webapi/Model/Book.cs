namespace CEOS_WebApi.Model
{
    public class Book : BaseModel
    {
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Anotation { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;

        public string Author
        {
            get
            {
                return AuthorFirstName + " " + (string.IsNullOrEmpty(AuthorMiddleName) ? "" : AuthorMiddleName + " ") +  AuthorLastName;
            }
        }
        public string AuthorFirstName { get; set; } = string.Empty;
        public string AuthorMiddleName { get; set; } = string.Empty;
        public string AuthorLastName { get; set; } = string.Empty;

        public int PublicationYear { get; set; }
    }
}
