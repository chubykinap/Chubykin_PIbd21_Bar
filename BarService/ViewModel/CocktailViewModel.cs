using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BarService.ViewModel
{
    [DataContract]
    public class CocktailViewModel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string CocktailName { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public List<ElementRequirementsViewModel> ElementRequirements { get; set; }
    }
}
