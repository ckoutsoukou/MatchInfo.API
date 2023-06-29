using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace MatchInfo.API.Utilities
{
    /// <summary>
    /// A Converter class for datetime according to specified format (used for data annotations).
    /// </summary>
    public class DateFormatConverterUtils : IsoDateTimeConverter
    {
        /// <summary>
        /// Ctor for DateFormatConverterUtils.
        /// </summary>
        /// <param name="format"></param>
        public DateFormatConverterUtils(string format)
        {
            DateTimeFormat = format;
        }
    }
}
