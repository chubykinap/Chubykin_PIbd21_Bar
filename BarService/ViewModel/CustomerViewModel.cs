using System.Runtime.Serialization;

namespace BarService.ViewModel
{
    [DataContract]
    public class CustomerViewModel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string CustomerFIO { get; set; }
    }
}
