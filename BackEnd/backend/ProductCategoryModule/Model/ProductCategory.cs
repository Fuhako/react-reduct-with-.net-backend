using System.ComponentModel.DataAnnotations;

namespace backend.ProductCategoryModule.Model
{
    public class ProductCategoryModel
    {
        [Key]
        public long id { get; set; }
        public string name { get; set; }
        public bool active { get; set; }
        public string created_user { get; set; }
        public DateTime created_date { get; set; }
        public string updated_user { get; set; }
        public string updated_date { get; set; }
    }
}
