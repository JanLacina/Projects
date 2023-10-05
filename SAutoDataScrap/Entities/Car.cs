using System.ComponentModel.DataAnnotations;

namespace SAutoDataScrap.Entities
{
    public class Car
    {
        public Car(string linkToDetail, int price, int mileage, int year, DateTime scrapDate)
        {
            LinkToDetail = linkToDetail;
            Price = price;
            Mileage = mileage;
            Year = year;
            ScrapDate = scrapDate;
            Specification = new List<Specification>();
            Equipment = new List<Equipment>();
        }

        public Car()
        {
            LinkToDetail = string.Empty;
            Specification = new List<Specification>();
            Equipment = new List<Equipment>();
        }

        public Guid Id { get; set; }

        [Required]
        public string LinkToDetail { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public DateTime ScrapDate { get; set; }

        [Required]
        public int Year { get; set; }

        public List<Specification> Specification { get; set; }
        public List<Equipment> Equipment { get; set; }
    }
}
