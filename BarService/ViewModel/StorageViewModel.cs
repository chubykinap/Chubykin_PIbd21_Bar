using System.Collections.Generic;

namespace BarService.ViewModel
{
    public class StorageViewModel
    {
        public int ID { get; set; }
        public string StorageName { get; set; }
        public List<ElementStorageViewModel> StorageElements { get; set; }
    }
}
