using BarService.BindingModels;
using BarService.ViewModel;
using System.Collections.Generic;

namespace BarService.Interfaces
{
    public interface IMessageInfo
    {
        List<MessageInfoViewModel> GetList();

        MessageInfoViewModel GetElement(int id);

        void AddElement(MessageInfoBindModel model);
    }
}