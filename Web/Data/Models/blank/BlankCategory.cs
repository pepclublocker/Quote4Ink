using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Data.Models
{
    [Table("BlankCategories")]
    public class BlankCategory
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int categoryID { get; set; }
        public string? name { get; set; }
        public string? image { get; set; }

    }
}
