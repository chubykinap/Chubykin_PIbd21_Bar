using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BarService.ViewModel
{
    [DataContract]
    public class StorageViewModel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string StorageName { get; set; }
        [DataMember]
        public List<ElementStorageViewModel> StorageElements { get; set; }
    }
}
