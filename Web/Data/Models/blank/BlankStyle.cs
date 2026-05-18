
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Data.Models
{
    [Table("BlankStyles")]
    public class BlankStyle
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int styleID { get; set; }
        public string? partNumber { get; set; }
        public string? brandName { get; set; }
        public string? styleName { get; set; }
        public string? uniqueStyleName { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? baseCategory { get; set; }
        public string? categories { get; set; }
        public string? catalogPageNumber { get; set; }
        public bool newStyle { get; set; } = false;
        public string? comparableGroup { get; set; }
        public string? companionGroup { get; set; }
        public string? brandImage { get; set; }
        public string? styleImage { get; set; }
        public string? noeRetailing { get; set; }
        public string? boxRequired { get; set; }

        public DateTime? dateLastChanged { get; set; } = DateTime.Now;

    }
}
