using WebScrapper.Entities;

namespace WebScrapper.Service
{
    public interface ICarService
    {
        IList<Car>? ScrapAndProcessData(string sAutoLink);
        bool SaveCars(IEnumerable<Car> collectionToSave);
    }
}
