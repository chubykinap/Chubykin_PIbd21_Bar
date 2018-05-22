using System.Runtime.Serialization;

namespace BarService.ViewModel
{
    [DataContract]
    public class ElementViewModel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string ElementName { get; set; }
    }
}
