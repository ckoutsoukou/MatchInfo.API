using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace MatchInfo.API.Utilities
{
    /// <summary>
    /// A Converter class for timespan (used for data annotations).
    /// </summary>
    public class TimespanFormatConverterUtils : JsonConverter<TimeSpan>
    {
        /// <summary>
        /// Format: Hours:Minutes.
        /// </summary>
        public const string TimeSpanFormatString = @"hh\:mm";

        /// <summary>
        /// Writes timespan formatted to writer.
        /// </summary>
        /// <param name="writer">The json writer.</param>
        /// <param name="value">The timespan value.</param>
        /// <param name="serializer">The json serializer.</param>
        public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
        {
            var timespanFormatted = $"{value.ToString(TimeSpanFormatString)}";
            writer.WriteValue(timespanFormatted);
        }

        /// <summary>
        /// Converts specified object to timespan according to specified format.
        /// </summary>
        /// <param name="reader">The json reader.</param>
        /// <param name="objectType">The type of object.</param>
        /// <param name="existingValue">The timespan existing value.</param>
        /// <param name="hasExistingValue">Indicates if has existing value.</param>
        /// <param name="serializer">The json serializer.</param>
        /// <returns>The parsed timespan</returns>
        /// <exception cref="JsonSerializationException">Throws JsonSerializationException.</exception>
        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            TimeSpan parsedTimeSpan;
            var hasParsed =TimeSpan.TryParseExact((string)reader.Value, TimeSpanFormatString, null, out parsedTimeSpan);
            if (!hasParsed) throw new JsonSerializationException("Unexpected value when converting time.");

            return parsedTimeSpan;
        }
    }
}
