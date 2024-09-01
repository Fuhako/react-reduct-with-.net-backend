using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.UserModule.Model
{
    [Table("m_user")]
    public class User
    {
        [Key]
        public long id { get; set; }
        public string user_id { get; set; } = "";
        public string password { get; set; } = "";
        public string email { get; set; } = "";
        public bool is_use { get; set; } = false;
        public bool is_lock { get; set; } = false;
        public bool active { get; set; } = true;
        public string created_user { get; set; } = "";
        public DateTime created_date { get; set; }
        public string updated_user { get; set; } = "";
        public DateTime updated_date { get; set; }
    }
}
