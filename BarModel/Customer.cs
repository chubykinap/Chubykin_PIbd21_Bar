using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarModel
{
    public class Customer
    {
        public int ID { set; get; }

        [Required]
        public string CustomerFIO { set; get; }

        [ForeignKey("CustomerID")]
        public virtual List<Order> Orders { get; set; }
    }
}
