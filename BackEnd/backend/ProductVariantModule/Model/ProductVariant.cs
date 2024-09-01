using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.ProductVariantModule.Model
{
    [Table("m_product_variant")]
    public class ProductVariant
    {
        [Key]
        public long id { get; set; }
        public long product_id { get; set; } = 0;
        public string code { get; set; } = "";
        public string name { get; set; } = "";
        public long qty { get; set; } = 0;
        public decimal price { get; set; } = 0;
        public bool active { get; set; } = true;
        public string created_user { get; set; } = "";
        public DateTime created_date { get; set; }
        public string updated_user { get; set; } = "";
        public DateTime updated_date { get; set; }
    }
}
