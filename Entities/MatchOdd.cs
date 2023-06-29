using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MatchInfo.API.Utilities;
using Microsoft.EntityFrameworkCore;

namespace MatchInfo.API.Entities
{
    [Index(nameof(MatchId), nameof(Specifier), IsUnique = true)]
    public class MatchOdd
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the match.
        /// </summary>
        [ForeignKey("MatchId")]
        public virtual Match Match { get; set; }

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
        [Range(1.0,1000000.0, ErrorMessage = "Odd exceeded given range (1.0,1000000.0)")]
        public double Odd { get; set; }
    }



}
