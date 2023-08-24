using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebScrapper.Service;

namespace WebScrapper
{
    public class WebScrapperProgram
    {
        static void Main(string[] args)
        {
            // Injections in console app
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddTransient<ICarService, CarService>()
                .BuildServiceProvider();

            // Logger initialization
            var logger = serviceProvider.GetService<ILoggerFactory>()?.CreateLogger<WebScrapperProgram>();
            if (logger == null)
            {
                throw new Exception("Was not possible to get ILogger service");
            }
            logger.LogInformation("Starting application");

            var service = serviceProvider.GetService<ICarService>();
            // Currently only for type of my car
            string link = "https://www.sauto.cz/inzerce/osobni/ford/focus?vyrobeno-od=2012&km-do=200000&vykon-od=175kw&palivo=benzin&vybava=vyhrivana-sedadla%2Ctempomat%2Cmultifunkcni-volant&razeni=od-nejlevnejsich";

            var result = service?.ScrapAndProcessData(link);

            if (result != null)
            {
                service?.SaveCars(result);
            }
        }
    }
}