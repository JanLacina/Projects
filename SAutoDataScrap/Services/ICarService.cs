using SAutoDataScrap.Entities;

namespace SAutoDataScrap.Services
{
    public interface ICarService
    {
        bool ScrapNewCars();
        IEnumerable<Car>? GetCars();
        IEnumerable<Car>? GetCarsByYear(int year);
        void DeleteDuplicities();
    }
}
