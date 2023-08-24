using WebScrapper.Entities;

namespace WebScrapper.Service
{
    public interface ICarService
    {
        IList<Car>? ScrapAndProcessData(string sAutoLink);
        bool SaveCars(IEnumerable<Car> collectionToSave);
        IEnumerable<Car>? GetCars();
        IEnumerable<Car>? GetCarsByYear(int year);
    }
}
