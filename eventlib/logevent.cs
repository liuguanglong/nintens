using Microsoft.Extensions.Logging;

namespace eventlib
{
    public static class logevent
    {
        //FetchData Event  1000+
        public static EventId WeatherForecastUpdated = new EventId(1001, "WeatherForecast.Updated");
        public static EventId WeatherForecastCreated = new EventId(1002, "WeatherForecast.Created");
        public static EventId WeatherForecastDeleted = new EventId(1003, "WeatherForecast.Deleted");
        public static EventId WeatherForecastSearched = new EventId(1004, "WeatherForecast.Searched");

    }
}