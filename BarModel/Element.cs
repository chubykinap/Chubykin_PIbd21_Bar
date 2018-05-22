using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarModel
{
    public class Element
    {
        public int ID { set; get; }

        [Required]
        public string ElementName { get; set; }

        [ForeignKey("ElementID")]
        public virtual List<ElementRequirement> ElementRequirement { get; set; }

        [ForeignKey("ElementID")]
        public virtual List<ElementStorage> ElementStorage { get; set; }
    }
}
