using BarService.Attributes;
using BarService.BindingModels;
using BarService.ViewModel;
using System.Collections.Generic;

namespace BarService.Interfaces
{
    [CustomInterface("Интерфейс для работы с коктейлями")]
    public interface ICocktail
    {
        [CustomMethod("Метод получения списка коктейлей")]
        List<CocktailViewModel> GetList();

        [CustomMethod("Метод получения коктейля по id")]
        CocktailViewModel GetElement(int id);

        [CustomMethod("Метод добавления коктейля")]
        void AddElement(CocktailBindModel model);

        [CustomMethod("Метод изменения данных коктейля")]
        void UpdElement(CocktailBindModel model);

        [CustomMethod("Метод удаления коктейля")]
        void DelElement(int id);
    }
}
