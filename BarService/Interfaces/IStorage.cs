using BarService.Attributes;
using BarService.BindingModels;
using BarService.ViewModel;
using System.Collections.Generic;

namespace BarService.Interfaces
{
    [CustomInterface("Интерфейс для работы с складами")]
    public interface IStorage
    {
        [CustomMethod("Метод получения списка складов")]
        List<StorageViewModel> GetList();

        [CustomMethod("Метод получения склада по id")]
        StorageViewModel GetElement(int id);

        [CustomMethod("Метод добавления склада")]
        void AddElement(StorageBindModel model);

        [CustomMethod("Метод изменения данных по складу")]
        void UpdElement(StorageBindModel model);

        [CustomMethod("Метод удаления склада")]
        void DelElement(int id);
    }
}
