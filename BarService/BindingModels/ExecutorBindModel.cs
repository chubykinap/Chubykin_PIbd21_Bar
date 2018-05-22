using System.Runtime.Serialization;

namespace BarService.BindingModels
{
    [DataContract]
    public class ExecutorBindModel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string ExecutorFIO { get; set; }
    }
}
