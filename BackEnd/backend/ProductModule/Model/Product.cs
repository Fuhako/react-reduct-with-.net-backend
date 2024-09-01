using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.ProductModule.Model
{
    [Table("m_product")]
    public class Product
    {
        [Key]
        public long id { get; set; }
        public string plu { get; set; } = "";
        public long product_category_id { get; set; } = 0;
        public bool active { get; set; } = true;
        public string created_user { get; set; } = "";
        public DateTime created_date { get; set; }
        public string updated_user { get; set; } = "";
        public DateTime updated_date { get; set; }
    }
}
