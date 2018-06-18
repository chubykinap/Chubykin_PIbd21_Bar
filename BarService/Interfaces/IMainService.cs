using BarService.BindingModels;
using BarService.ViewModel;
using System.Collections.Generic;

namespace BarService.Interfaces
{
    public interface IMainService
    {
        List<OrderViewModel> GetList();

        void CreateOrder(OrderBindModel model);

        void TakeOrderInWork(OrderBindModel model);

        void FinishOrder(int id);

        void PayOrder(int id);

        void PutElementOnStorage(ElementStorageBindModel model);
    }
}
