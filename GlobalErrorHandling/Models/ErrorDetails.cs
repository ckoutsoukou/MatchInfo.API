using System.Text.Json;

namespace MatchInfo.API.GlobalErrorHandling.Models
{
    /// <summary>
    /// A class for global handling errors.
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// Gets or sets the ttatus code.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Overrides string method.
        /// </summary>
        /// <returns>The class in json format.</returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
