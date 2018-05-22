using System;

namespace BarService.BindingModels
{
    public class ReportBindModel
    {
        public string FileName { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
