using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MatchInfo.API.Utilities
{
    /// <summary>
    /// Class in order to specify values for a string property (used for data annotations).
    /// </summary>
    public class StringRangeAttributeUtils : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets the allowable values for a string.
        /// </summary>
        public string[] AllowableValues { get; set; }

        /// <summary>
        /// Checks if the value of specified property is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>Indicates if the value of specified property is valid.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (AllowableValues?.Contains(value?.ToString()) == true)
            {
                return ValidationResult.Success;
            }

            var msg = $"Please enter one of the allowable values: {string.Join(", ", (AllowableValues ?? new string[] { "No allowable values found" }))}.";
            return new ValidationResult(msg);
        }
    }
}
