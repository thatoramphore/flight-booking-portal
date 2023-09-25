using Flights.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace Flights.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly ILogger<FlightController> _logger;

        static Random random = new Random();

        static private FlightRm[] flights = new FlightRm[]

            {
        new (   Guid.NewGuid(),
                "British Airways",
                random.Next(15000, 20000).ToString(),
                new TimePlaceRm("London, England",DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlaceRm("Johannesburg",DateTime.Now.AddHours(random.Next(4, 10))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Emirates",
                random.Next(15000, 25000).ToString(),
                new TimePlaceRm("Dubai",DateTime.Now.AddHours(random.Next(1, 10))),
                new TimePlaceRm("Nairobi, Kenya",DateTime.Now.AddHours(random.Next(4, 15))),
                random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "British Airways",
                random.Next(19000, 25000).ToString(),
                new TimePlaceRm("London, England",DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlaceRm("Durban",DateTime.Now.AddHours(random.Next(4, 18))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "CemAir",
                random.Next(10000, 25000).ToString(),
                new TimePlaceRm("Cape Town",DateTime.Now.AddHours(random.Next(1, 21))),
                new TimePlaceRm("Cairo, Egypt",DateTime.Now.AddHours(random.Next(4, 21))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Kulula",
                random.Next(9000, 12000).ToString(),
                new TimePlaceRm("Gqheberha",DateTime.Now.AddHours(random.Next(1, 23))),
                new TimePlaceRm("Mauritius",DateTime.Now.AddHours(random.Next(4, 25))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Africa  World Airlines",
                random.Next(9000, 15000).ToString(),
                new TimePlaceRm("Bloemfontein",DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlaceRm("Seychelles",DateTime.Now.AddHours(random.Next(4, 19))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Kenya Airways",
                random.Next(9000, 15000).ToString(),
                new TimePlaceRm("Nairobi, Kenya",DateTime.Now.AddHours(random.Next(1, 55))),
                new TimePlaceRm("Polokwane",DateTime.Now.AddHours(random.Next(4, 58))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Airlink",
                random.Next(9000, 15000).ToString(),
                new TimePlaceRm("Bloemfontein",DateTime.Now.AddHours(random.Next(1, 58))),
                new TimePlaceRm("Barcelona, Spain",DateTime.Now.AddHours(random.Next(4, 60))),
                    random.Next(1, 853))
            };


        public FlightController(ILogger<FlightController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(FlightRm), 200)]
        public IEnumerable<FlightRm> Search()
            => flights;

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(FlightRm), 200)]
        [HttpGet("{id}")]
        public ActionResult<FlightRm> Find(Guid id)
        {
            var flight = flights.SingleOrDefault(f => f.Id == id);

            if (flight == null) return NotFound();

            return Ok(flight);
        }
    }
}
