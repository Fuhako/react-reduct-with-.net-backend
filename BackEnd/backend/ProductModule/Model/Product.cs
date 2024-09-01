using System.ComponentModel.DataAnnotations;

namespace backend.ProductModule.Model
{
    public class ProductModel
    {
        [Key]
        public long id { get; set; }
        public string plu { get; set; }
        public long product_category_id { get; set; }
        public bool active { get; set; }
        public string created_user { get; set; }
        public DateTime created_date { get; set; }
        public string updated_user { get; set; }
        public string updated_date { get; set; }
    }
}
