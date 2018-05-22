using System.Runtime.Serialization;

namespace BarService.BindingModels
{
    [DataContract]
    public class ElementStorageBindModel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int StorageID { get; set; }
        [DataMember]
        public int ElementID { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
