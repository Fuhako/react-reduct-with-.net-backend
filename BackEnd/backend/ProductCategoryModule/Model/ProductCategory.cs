using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.ProductCategoryModule.Model
{
    [Table("m_product_category")]
    public class ProductCategory
    {
        [Key]
        public long id { get; set; }
        public string name { get; set; } = "";
        public bool active { get; set; } = true;
        public string created_user { get; set; } = "";
        public DateTime created_date { get; set; }
        public string updated_user { get; set; } = "";
        public DateTime updated_date { get; set; }
    }
}
