using Microsoft.EntityFrameworkCore;
using SAutoDataScrap.Data;
using SAutoDataScrap.Entities;

namespace SAutoDataScrap.Repository
{
    public class CarRepository
    {
        private readonly CarDbContext context;
        private readonly ILogger<CarRepository> logger;

        public CarRepository(ILogger<CarRepository> logger)
        {
            context = new CarDbContext();
            this.logger = logger;
        }

        public bool SaveCars(IEnumerable<Car> collectionToSave)
        {
            try
            {
                context.Database.EnsureCreated();
                context.AddRange(collectionToSave);
                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Was not possible to save cars");
                return false;
            }
        }

        public void DeleteDuplicities()
        {
            try
            {
                var cars = context.Cars;
                var equipment = context.Equipment;
                var specifications = context.Specification;

                var groups = cars.ToList().GroupBy(item => new { DateTime = new DateTime(item.ScrapDate.Year, item.ScrapDate.Month, item.ScrapDate.Day), item.LinkToDetail }).Select(group => group.First()).ToList();
                var duplicities = cars.ToList().Except(groups);

                context.Cars.RemoveRange(duplicities);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Was not possible to delete duplicities");
            }
        }

        public IEnumerable<Car> GetAllCars()
        {
            try
            {
                var cars = context.Cars
                    .Include(e => e.Equipment)
                    .Include(s => s.Specification);

                return cars.ToList();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Was not possible to return cars");
                return null;
            }
        }

        public IEnumerable<Car> GetCarsByYear(int year)
        {
            try
            {
                var cars = context.Cars
                    .Include(e => e.Equipment)
                    .Include(s => s.Specification)
                    .Where(x => x.Year.Equals(year))
                    .ToList();

                return cars;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Was not possible to return cars by year");
                return null;
            }
        }
    }
}
