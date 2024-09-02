using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.MenuModule.Model
{
    [Table("m_menu")]
    public class Menu
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
