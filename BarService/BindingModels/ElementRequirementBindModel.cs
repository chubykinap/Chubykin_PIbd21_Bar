using System.Runtime.Serialization;

namespace BarService.BindingModels
{
    [DataContract]
    public class ElementRequirementsBindModel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int CocktailID { get; set; }
        [DataMember]
        public int ElementID { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
