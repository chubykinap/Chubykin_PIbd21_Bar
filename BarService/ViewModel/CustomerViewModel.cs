using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BarService.ViewModel
{
    [DataContract]
    public class CustomerViewModel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Mail { get; set; }
        [DataMember]
        public string CustomerFIO { get; set; }
        [DataMember]
        public List<MessageInfoViewModel> Messages { get; set; }
    }
}
