using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.ProductVariantModule.Model
{
    [Table("m_product_variant")]
    public class ProductVariant
    {
        [Key]
        public long id { get; set; }
        public long product_id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public long qty { get; set; }
        public decimal price { get; set; }
        public bool active { get; set; }
        public string created_user { get; set; }
        public DateTime created_date { get; set; }
        public string updated_user { get; set; }
        public string updated_date { get; set; }
    }
}
