using System.Collections.Generic;

namespace BarService.ViewModel
{
    public class CocktailViewModel
    {
        public int ID { get; set; }
        public string CocktailName { get; set; }
        public decimal Price { get; set; }
        public List<ElementRequirementsViewModel> ElementRequirements { get; set; }
    }
}
