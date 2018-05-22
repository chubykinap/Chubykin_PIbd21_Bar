using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarModel
{
    public class Executor
    {
        public int ID { get; set; }

        [Required]
        public string ExecutorFIO { get; set; }

        [ForeignKey("ExecutorID")]
        public virtual List<Order> Orders { get; set; }
    }
}
