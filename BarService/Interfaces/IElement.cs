using BarService.BindingModels;
using BarService.ViewModel;
using System.Collections.Generic;

namespace BarService.Interfaces
{
    public interface IElement
    {
        List<ElementViewModel> GetList();

        ElementViewModel GetElement(int id);

        void AddElement(ElementBindModel model);

        void UpdElement(ElementBindModel model);

        void DelElement(int id);
    }
}
