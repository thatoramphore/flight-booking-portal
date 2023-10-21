using Flights.ReadModels;
using Flights.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Flights.DTOs;

namespace Flights.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly ILogger<FlightController> _logger;

        static Random random = new Random();

        static private Flight[] flights = new Flight[]
            {
        new (   Guid.NewGuid(),
                "British Airways",
                random.Next(15000, 20000).ToString(),
                new TimePlace("London, England",DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlace("Johannesburg",DateTime.Now.AddHours(random.Next(4, 10))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Emirates",
                random.Next(15000, 25000).ToString(),
                new TimePlace("Dubai",DateTime.Now.AddHours(random.Next(1, 10))),
                new TimePlace("Nairobi, Kenya",DateTime.Now.AddHours(random.Next(4, 15))),
                random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "British Airways",
                random.Next(19000, 25000).ToString(),
                new TimePlace("London, England",DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlace("Durban",DateTime.Now.AddHours(random.Next(4, 18))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "CemAir",
                random.Next(10000, 25000).ToString(),
                new TimePlace("Cape Town",DateTime.Now.AddHours(random.Next(1, 21))),
                new TimePlace("Cairo, Egypt",DateTime.Now.AddHours(random.Next(4, 21))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Kulula",
                random.Next(9000, 12000).ToString(),
                new TimePlace("Gqheberha",DateTime.Now.AddHours(random.Next(1, 23))),
                new TimePlace("Mauritius",DateTime.Now.AddHours(random.Next(4, 25))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Africa  World Airlines",
                random.Next(9000, 15000).ToString(),
                new TimePlace("Bloemfontein",DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlace("Seychelles",DateTime.Now.AddHours(random.Next(4, 19))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Kenya Airways",
                random.Next(9000, 15000).ToString(),
                new TimePlace("Nairobi, Kenya",DateTime.Now.AddHours(random.Next(1, 55))),
                new TimePlace("Polokwane",DateTime.Now.AddHours(random.Next(4, 58))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Airlink",
                random.Next(9000, 15000).ToString(),
                new TimePlace("Bloemfontein",DateTime.Now.AddHours(random.Next(1, 58))),
                new TimePlace("Barcelona, Spain",DateTime.Now.AddHours(random.Next(4, 60))),
                    random.Next(1, 853))
            };

        

        public FlightController(ILogger<FlightController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(IEnumerable<FlightRm>), 200)]
        public IEnumerable<FlightRm> Search()
        {
            var flightRmList = flights.Select(flight => new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
                flight.RemainingNumberOfSeats
                ));

            return flightRmList;
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(FlightRm), 200)]
        public ActionResult<FlightRm> Find(Guid id)
        {
            var flight = flights.SingleOrDefault(f => f.Id == id);

            if (flight == null)
                return NotFound();

            var readModel = new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
                flight.RemainingNumberOfSeats
                );

            return Ok(readModel);
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(200)]
        public IActionResult Book(BookDTO dto)
        {
            System.Diagnostics.Debug.WriteLine($"Booking a new flight: {dto.FlightId}");

            var flight = flights.SingleOrDefault(f => f.Id == dto.FlightId);

            if (flight == null) return NotFound();

            flight.Bookings.Add(
                new Booking(
                    dto.FlightId,
                    dto.PassengerEmail,
                    dto.NumberOfSeats
                    )
                );
            return CreatedAtAction(nameof(Find), new { id = dto.FlightId });
        }

    }
}