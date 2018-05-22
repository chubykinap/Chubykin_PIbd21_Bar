using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BarService.ViewModel
{
    [DataContract]
    public class StorageLoadViewModel
    {
        [DataMember]
        public string StorageName { get; set; }
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public List<StorageElementLoadViewModel> Elements { get; set; }
    }

    [DataContract]
    public class StorageElementLoadViewModel
    {
        [DataMember]
        public string ElementName { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
