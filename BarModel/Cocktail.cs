using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarModel
{
    public class Cocktail
    {
        public int ID { set; get; }

        [Required]
        public string CocktailName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("CocktailID")]
        public virtual List<Order> Orders { set; get; }

        [ForeignKey("CocktailID")]
        public virtual List<ElementRequirement> ElementRequirements { set; get; }
    }
}
