using Microsoft.Extensions.Logging;
using WebScrapper.Data;
using WebScrapper.Entities;

namespace WebScrapper.Repository
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

        public IEnumerable<Car>? GetAllCars()
        {
            try
            {
                var query = context.Cars.AsQueryable();

                return query.ToList();
            }
            catch(Exception e)
            {
                logger.LogError(e, "Was not possible to return cars");
                return null;
            }
        }

        public IEnumerable<Car>? GetCarsByYear(int year)
        {
            try
            {
                var query = GetAllCars()?.Where(x=>x.Year.Equals(year));

                return query?.ToList();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Was not possible to return cars by year");
                return null;
            }
        }
    }
}
