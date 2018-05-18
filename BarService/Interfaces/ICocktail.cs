using BarService.BindingModels;
using BarService.ViewModel;
using System.Collections.Generic;

namespace BarService.Interfaces
{
    public interface ICocktail
    {
        List<CocktailViewModel> GetList();

        CocktailViewModel GetElement(int id);

        void AddElement(CocktailBindModel model);

        void UpdElement(CocktailBindModel model);

        void DelElement(int id);
    }
}
