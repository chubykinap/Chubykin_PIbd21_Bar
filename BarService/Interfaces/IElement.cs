using BarService.Attributes;
using BarService.BindingModels;
using BarService.ViewModel;
using System.Collections.Generic;

namespace BarService.Interfaces
{
    [CustomInterface("Интерфейс для работы с компонентами")]
    public interface IElement
    {
        [CustomMethod("Метод получения списка компонент")]
        List<ElementViewModel> GetList();

        [CustomMethod("Метод получения компонента по id")]
        ElementViewModel GetElement(int id);

        [CustomMethod("Метод добавления компонента")]
        void AddElement(ElementBindModel model);

        [CustomMethod("Метод изменения данных компонента")]
        void UpdElement(ElementBindModel model);

        [CustomMethod("Метод удаления компонента")]
        void DelElement(int id);
    }
}
