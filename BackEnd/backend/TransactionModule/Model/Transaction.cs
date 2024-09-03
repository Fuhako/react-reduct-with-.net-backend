using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.TransactionModule.Model
{
    [Table("m_transaction")]
    public class Transaction
    {
        [Key]
        public long id { get; set; }
        public string transaction_no { get; set; } = "";
        public decimal total_amount { get; set; } = 0;
        public  bool active { get; set; } = true;
        public string created_user { get; set; } = "";
        public DateTime created_date { get; set; }
        public string updated_user { get; set; } = "";
        public DateTime updated_date { get; set; }
    }
}
