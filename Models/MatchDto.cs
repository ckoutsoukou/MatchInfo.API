using MatchInfo.API.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Runtime.Serialization;

namespace MatchInfo.API.Models
{
    /// <summary>
    /// A class for Match
    /// </summary>
    public class MatchDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the Team A.
        /// </summary>
        [Required(ErrorMessage = "TeamA is required")]
        [StringLength(30, ErrorMessage = "TeamA exceeded string length (30)")]
        public string TeamA { get; set; }

        /// <summary>
        /// Gets or sets the Team B.
        /// </summary>
        [Required(ErrorMessage = "TeamB is required")]
        [StringLength(30, ErrorMessage = "TeamB exceeded string length (30)")]
        public string TeamB { get; set; }

        /// <summary>
        /// Gets or sets the match date (dd/MM/yyyy).
        /// </summary>
        [Required(ErrorMessage = "MatchDate is required")]
        [JsonConverter(typeof(DateFormatConverterUtils), "dd/MM/yyyy")]
        public DateTime MatchDate { get; set; }

        /// <summary>
        /// Gets or setsthe match time (hh:mm).
        /// </summary>
        [Required(ErrorMessage = "MatchTime is required")]
        [JsonConverter(typeof(TimespanFormatConverterUtils))]
        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public TimeSpan MatchTime { get; set; }

        /// <summary>
        /// Gets or sets the Sport Category.
        /// </summary>
        [Required(ErrorMessage = "Sport category is required ( \"Football\", \"Basketball\")")]
        [StringRangeAttributeUtils(AllowableValues = new[] { "Football", "Basketball" }, ErrorMessage = "Sport must be either 'Football' or 'Basketball'.")]
        public string SportCategory { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        [StringLength(50, ErrorMessage = "Specifier exceeded string length (50)")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the list of match odds.
        /// </summary>
        public List<MatchOddDto> MatchOddDtos { get; set; } = new List<MatchOddDto>();

    }

    /// <summary>
    /// An enum for Sport Category
    /// </summary>
    public enum SportCategory
    {
        /// <summary>
        /// Unknown sport.
        /// </summary>
        [Description("")]
        [Category("")]
        Unknown,
        /// <summary>
        /// Football,
        /// </summary>
        [Description("Football")]
        [Category("Football")]
        Football,
        /// <summary>
        /// Basketball
        /// </summary>
        [Description("Basketball")]
        [Category("Basketball")]
        Basketball,
    }


}
