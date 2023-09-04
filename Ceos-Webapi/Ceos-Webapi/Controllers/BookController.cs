using CEOS_WebApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CEOS_WebApi.Controllers
{
    [ApiController]
    [Route("api/books")]
    [Produces("application/json")]
    public class BookController : ControllerBase
    {
        private readonly BooksRepository booksRepository = new BooksRepository();

        public BookController()
        {
        }

        [HttpGet]
        public JsonResult GetBooks(int skip = 0, int pageSize = 0)
        {
            var books = booksRepository.GetBooks();

            //see Readme, proper filtering I would do via DataSourceRequest or similar way
            var result = books.Skip(skip);

            if(pageSize > 0)
            {
                return new JsonResult(result.Take(pageSize));
            }

            return new JsonResult(result);
        }
    }
}