using BarService.Attributes;
using BarService.BindingModels;
using BarService.ViewModel;
using System.Collections.Generic;

namespace BarService.Interfaces
{
    [CustomInterface("Интерфейс для работы с отчетами")]
    public interface IReportService
    {
        [CustomMethod("Метод сохранения списка изделий в doc-файл")]
        void SaveCocktailPrice(ReportBindModel model);

        [CustomMethod("Метод получения списка складов с количество компонент на них")]
        List<StorageLoadViewModel> GetStoragesLoad();

        [CustomMethod("Метод сохранения списка списка складов с количество компонент на них в xls-файл")]
        void SaveStoragesLoad(ReportBindModel model);

        [CustomMethod("Метод получения списка заказов клиентов")]
        List<CustomerOrderViewModel> GetCustomerOrders(ReportBindModel model);

        [CustomMethod("Метод сохранения списка заказов клиентов в pdf-файл")]
        void SaveCustomerOrders(ReportBindModel model);
    }
}
