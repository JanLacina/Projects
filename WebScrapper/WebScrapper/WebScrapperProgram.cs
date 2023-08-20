using WebScrapper.Service;

namespace WebScrapper
{
    //https://zzzcode.ai/answer-question?p1=Html+Agility+Pack&p2=How+to+do+selector+when+I+want+all+%22ul%22+element+which+child+%22li%22+element+have+classes+%22c-item++c-item--hor%22.+Those+%22li%22+which+contains+class+%22c-preferred-list%22+I+want+to+skip.
    //https://html-agility-pack.net/documentation
    public class WebScrapperProgram
    {
        static void Main(string[] args)
        {
            // Currently only for type of my car
            string link = "https://www.sauto.cz/inzerce/osobni/ford/focus?vyrobeno-od=2012&km-do=200000&vykon-od=175kw&palivo=benzin&vybava=vyhrivana-sedadla%2Ctempomat%2Cmultifunkcni-volant&razeni=od-nejlevnejsich";
            var service = new CarService();
            var result = service.ScrapAndProcessData(link);

            if(result != null)
            {
                var savedSucessfully = service.SaveCars(result);

                if (!savedSucessfully)
                {
                    Console.WriteLine($"{DateTime.UtcNow} - Was not properly saved.");
                }
            }
        }
    }
}