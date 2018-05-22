using BarService.BindingModels;
using BarService.ViewModel;
using System.Collections.Generic;

namespace BarService.Interfaces
{
    public interface IReportService
    {
        void SaveCocktailPrice(ReportBindModel model);

        List<StorageLoadViewModel> GetStoragesLoad();

        void SaveStoragesLoad(ReportBindModel model);

        List<CustomerOrderViewModel> GetCustomerOrders(ReportBindModel model);

        void SaveCustomerOrders(ReportBindModel model);
    }
}
