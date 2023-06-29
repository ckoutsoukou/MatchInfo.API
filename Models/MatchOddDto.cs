using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MatchInfo.API.Models
{
    /// <summary>
    /// A class for MatchOddDto.
    /// </summary>
    public class MatchOddDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the match.
        /// </summary>
        [JsonIgnore]
        public MatchDto Match { get; set; }

        /// <summary>
        /// Gets or sets the match identifier.
        /// </summary>
        public int MatchId { get; set; }

        /// <summary>
        /// Gets or sets the specifier.
        /// </summary>
        [Required(ErrorMessage = "Specifier is required")]
        [StringLength(20, ErrorMessage = "Specifier exceeded string length (20)")]
        public string Specifier { get; set; }

        /// <summary>
        /// Gets or sets the odd.
        /// </summary>
        [Required(ErrorMessage = "Odd is required")]
        [Range(1.0, 1000000.0, ErrorMessage = "Odd exceeded given range (1.0,1000000.0)")]
        public double Odd { get; set; }
    }
}
