using System.ComponentModel;

namespace Flights.DTOs
{
    public record FlightSearchParameters(

        [DefaultValue("07/07/2024 10:45:00 AM")]
        DateTime? FromDate,

        [DefaultValue("08/08/2024 10:45:00 AM")]
        DateTime? ToDate,

        [DefaultValue("Cape Town")]
        string? From,

        [DefaultValue("Cairo")]
        string? Destination,

        [DefaultValue(1)]
        int? NumberOfPassengers
        );
    
}
