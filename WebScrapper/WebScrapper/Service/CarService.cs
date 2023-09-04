using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using WebScrapper.Entities;
using WebScrapper.Repository;

namespace WebScrapper.Service
{
    public class CarService : ICarService
    {
        public readonly CarRepository repository;
        private readonly ILogger<CarRepository> logger;

        public CarService(ILogger<CarRepository> logger)
        {
            repository = new CarRepository(logger);
            this.logger = logger;
        }

        /// <summary>
        /// From URL with parameters to SAuto scrap data, process it to object Car and returns collection of Car object
        /// </summary>
        /// <param name="sAutoLink">URL to SAuto</param>
        /// <returns>Collection of Car</returns>
        /// <exception cref="NullReferenceException"></exception>
        public IList<Car>? ScrapAndProcessData(string sAutoLink)
        {
            try
            {
                logger.LogInformation("Scrapping started");
                var web = new HtmlWeb();
                IList<Car> carList = new List<Car>();

                // Static link to specifically equiped Ford Focus ST mk3
                var document = web.Load("https://www.sauto.cz/inzerce/osobni/ford/focus?vyrobeno-od=2012&km-do=200000&vykon-od=175kw&palivo=benzin&vybava=vyhrivana-sedadla%2Ctempomat%2Cmultifunkcni-volant&razeni=od-nejlevnejsich");

                int carCount = 0;

                if (document == null)
                {
                    Console.WriteLine($"{DateTime.UtcNow} - Was not possible to get data from SAuto.");
                    return null;
                }

                var items = document.DocumentNode.SelectNodes("//ul[contains(@class, 'c-item-list__list')]").FirstOrDefault()?.ChildNodes;
                var filteredItems = items?.Where(x => x.HasClass("c-item"));
                if (filteredItems != null)
                {
                    carCount = filteredItems.Count();

                    int indexOfCar = 1;
                    foreach (var item in filteredItems)
                    {
                        Car toAdd = new();

                        var carObject = item.SelectNodes($"/html[1]/body[1]/div[2]/div[2]/div[2]/div[2]/div[1]/div[2]/div[4]/ul[1]/li[{indexOfCar}]/div[1]/div[1]/div[1]");

                        var link = carObject
                            .SelectMany(x => x.ChildNodes)
                            .FirstOrDefault(y => y.Name.Equals("a"))?
                            .GetAttributeValue("href", "#");

                        if (link == null)
                        {
                            throw new NullReferenceException();
                        }

                        toAdd.ScrapDate = DateTime.Now;
                        toAdd.LinkToDetail = link;

                        var carInfo = carObject.SelectMany(x => x.ChildNodes).Where(y => y.Name.Equals("div"));

                        // First div is Price and mileage, second 
                        foreach (var subInfo in carInfo)
                        {
                            if (subInfo.HasClass("c-item__info-wrap"))
                            {
                                var yearAndMileage = subInfo.ChildNodes.FirstOrDefault(x => x.Name.Equals("div"))?.InnerText;
                                if (yearAndMileage != null)
                                {
                                    var splittedText = yearAndMileage.Split(',');
                                    int.TryParse(splittedText[0], out int year);
                                    int.TryParse(string.Join("", splittedText[1].Where(x => char.IsNumber(x))), out int mileage);

                                    toAdd.Year = year;
                                    toAdd.Mileage = mileage;
                                }
                            }
                            else if (subInfo.HasClass("c-item__data"))
                            {
                                int.TryParse(string.Join("", subInfo.InnerText.Substring(0, subInfo.InnerText.IndexOf("Kč")).Where(x => char.IsNumber(x))), out int price);
                                toAdd.Price = price;
                            }
                        }

                        IList<string> skippedSpecs = new List<string> { "Stav:", "Najeto:", "Vyrobeno:", "V provozu od:", "1. majitel:", "Serv. knížka:", "Ekologická daň:" };

                        var carDetails = web.Load(toAdd.LinkToDetail);
                        var carSpecs = carDetails.DocumentNode.SelectNodes("//tr[contains(@class, 'c-car-details-section__table-row')]");
                        var carEquipment = carDetails.DocumentNode.SelectNodes("//tr[contains(@class, 'c-car-details-section__equipment-row')]");

                        // Specifications
                        foreach (var spec in carSpecs)
                        {
                            if (spec.ChildNodes.Any(x => skippedSpecs.Contains(x.InnerText)))
                            {
                                continue;
                            }
                            var name = spec.ChildNodes[0].InnerText;
                            var value = spec.ChildNodes[1].InnerText;

                            toAdd.Specification.Add(new Specification { Name = name, Value = value });
                        }

                        // Equipment
                        foreach (var equipment in carEquipment)
                        {
                            var name = equipment.ChildNodes[0].InnerText;
                            var value = equipment.ChildNodes[1].InnerText;

                            toAdd.Equipment.Add(new Equipment { Name = name, Value = value });
                        }

                        indexOfCar++;
                        carList.Add(toAdd);
                    }
                }

                logger.LogInformation("Scrapping done");
                return carList;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Was not possible to scrap new data");
                return null;
            }
        }

        /// <summary>
        /// Takes collection of Car object, validate, save
        /// </summary>
        /// <param name="collectionToSave">Collection of Car</param>
        /// <returns>True if properly saved</returns>
        /// <exception cref="NullReferenceException"></exception>
        public bool SaveCars(IEnumerable<Car> collectionToSave)
        {
            if (collectionToSave == null) throw new NullReferenceException();

            try
            {
                repository.SaveCars(collectionToSave);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"{DateTime.UtcNow} - Was not possible to save to database.", ex.ToString());
                return false;
            }
        }

        public IEnumerable<Car>? GetCars()
        {
            try
            {
                var data = repository.GetAllCars();
                return data;
            }
            catch (Exception ex)
            {
                logger.LogError($"{DateTime.UtcNow} - Was not possible to save to database.", ex.ToString());
                return null;
            }
        }

        public IEnumerable<Car>? GetCarsByYear(int year)
        {
            try
            {
                var data = repository.GetCarsByYear(year);
                return data;
            }
            catch (Exception ex)
            {
                logger.LogError($"{DateTime.UtcNow} - Was not possible to save to database.", ex.ToString());
                return null;
            }
        }
    }
}
