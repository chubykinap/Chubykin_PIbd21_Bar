using System.Runtime.Serialization;

namespace BarService.BindingModels
{
    [DataContract]
    public class CustomerBindModel
    {
        [DataMember]
        public int ID { set; get; }
        [DataMember]
        public string CustomerFIO { set; get; }
    }
}
