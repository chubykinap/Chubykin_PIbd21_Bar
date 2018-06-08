using BarService.Attributes;
using BarService.BindingModels;
using BarService.ViewModel;
using System.Collections.Generic;

namespace BarService.Interfaces
{
    [CustomInterface("Интерфейс для работы с письмами")]
    public interface IMessageInfo
    {
        [CustomMethod("Метод получения списка писем")]
        List<MessageInfoViewModel> GetList();

        [CustomMethod("Метод получения письма по id")]
        MessageInfoViewModel GetElement(int id);

        [CustomMethod("Метод добавления письма")]
        void AddElement(MessageInfoBindModel model);
    }
}