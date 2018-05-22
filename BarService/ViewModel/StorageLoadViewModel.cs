using System;
using System.Collections.Generic;

namespace BarService.ViewModel
{
    public class StorageLoadViewModel
    {
        public string StorageName { get; set; }

        public int TotalCount { get; set; }

        public IEnumerable<Tuple<string, int>> Elements { get; set; }
    }
}
