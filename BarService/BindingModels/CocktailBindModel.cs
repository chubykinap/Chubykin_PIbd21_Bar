using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BarService.BindingModels
{
    [DataContract]
    public class CocktailBindModel
    {
        [DataMember]
        public int ID { set; get; }
        [DataMember]
        public string CocktailName { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public List<ElementRequirementsBindModel> ElementRequirements { get; set; }
    }
}
