using System.Runtime.Serialization;

namespace BarService.ViewModel
{
    [DataContract]
    public class ElementRequirementsViewModel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int CocktailID { get; set; }
        [DataMember]
        public int ElementID { get; set; }
        [DataMember]
        public string ElementName { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
