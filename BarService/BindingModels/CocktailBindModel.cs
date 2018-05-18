using System.Collections.Generic;

namespace BarService.BindingModels
{
    public class CocktailBindModel
    {
        public int ID { set; get; }
        public string CocktailName { get; set; }
        public decimal Price { get; set; }
        public List<ElementRequirementsBindModel> ElementRequirements { get; set; }
    }
}
