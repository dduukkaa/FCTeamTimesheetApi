using Newtonsoft.Json.Converters;

namespace FCTeamTimesheet.Configuration
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
