using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.MenuAccessModule.Model
{
    [Table("m_menu_access")]
    public class MenuAccess
    {
        [Key]
        public long id { get; set; }
        public long menu_id { get; set; }
        public long role_id { get; set; }
        public bool active { get; set; } = true;
        public string created_user { get; set; } = "";
        public DateTime created_date { get; set; }
        public string updated_user { get; set; } = "";
        public DateTime updated_date { get; set; }
    }
}
