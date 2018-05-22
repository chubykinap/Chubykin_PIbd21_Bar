using System.Runtime.Serialization;

namespace BarService.BindingModels
{
    [DataContract]
    public class ElementBindModel
    {
        [DataMember]
        public int ID { set; get; }
        [DataMember]
        public string ElementName { get; set; }
    }
}
