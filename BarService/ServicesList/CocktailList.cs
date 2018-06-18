using BarService.Interfaces;
using System;
using System.Collections.Generic;
using BarService.BindingModels;
using BarService.ViewModel;
using BarModel;

namespace BarService.ServicesList
{
    public class CocktailList : ICocktail
    {
        private DataListSingleton source;

        public CocktailList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<CocktailViewModel> GetList()
        {
            List<CocktailViewModel> result = new List<CocktailViewModel>();
            for (int i = 0; i < source.Cocktails.Count; ++i)
            {
                List<ElementRequirementsViewModel> ElementRequirements = new List<ElementRequirementsViewModel>();
                for (int j = 0; j < source.ElementRequirements.Count; ++j)
                {
                    if (source.ElementRequirements[j].CocktailID == source.Cocktails[i].ID)
                    {
                        string ElementName = string.Empty;
                        for (int k = 0; k < source.Elements.Count; ++k)
                        {
                            if (source.ElementRequirements[j].ElementID == source.Elements[k].ID)
                            {
                                ElementName = source.Elements[k].ElementName;
                                break;
                            }
                        }
                        ElementRequirements.Add(new ElementRequirementsViewModel
                        {
                            ID = source.ElementRequirements[j].ID,
                            CocktailID = source.ElementRequirements[j].CocktailID,
                            ElementID = source.ElementRequirements[j].ElementID,
                            ElementName = ElementName,
                            Count = source.ElementRequirements[j].Count
                        });
                    }
                }
                result.Add(new CocktailViewModel
                {
                    ID = source.Cocktails[i].ID,
                    CocktailName = source.Cocktails[i].CocktailName,
                    Price = source.Cocktails[i].Price,
                    ElementRequirements = ElementRequirements
                });
            }
            return result;
        }

        public CocktailViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Cocktails.Count; ++i)
            {
                List<ElementRequirementsViewModel> ElementRequirements = new List<ElementRequirementsViewModel>();
                for (int j = 0; j < source.ElementRequirements.Count; ++j)
                {
                    if (source.ElementRequirements[j].CocktailID == source.Cocktails[i].ID)
                    {
                        string ElementName = string.Empty;
                        for (int k = 0; k < source.Elements.Count; ++k)
                        {
                            if (source.ElementRequirements[j].ElementID == source.Elements[k].ID)
                            {
                                ElementName = source.Elements[k].ElementName;
                                break;
                            }
                        }
                        ElementRequirements.Add(new ElementRequirementsViewModel
                        {
                            ID = source.ElementRequirements[j].ID,
                            CocktailID = source.ElementRequirements[j].CocktailID,
                            ElementID = source.ElementRequirements[j].ElementID,
                            ElementName = ElementName,
                            Count = source.ElementRequirements[j].Count
                        });
                    }
                }
                if (source.Cocktails[i].ID == id)
                {
                    return new CocktailViewModel
                    {
                        ID = source.Cocktails[i].ID,
                        CocktailName = source.Cocktails[i].CocktailName,
                        Price = source.Cocktails[i].Price,
                        ElementRequirements = ElementRequirements
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(CocktailBindModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Cocktails.Count; ++i)
            {
                if (source.Cocktails[i].ID > maxId)
                {
                    maxId = source.Cocktails[i].ID;
                }
                if (source.Cocktails[i].CocktailName == model.CocktailName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Cocktails.Add(new Cocktail
            {
                ID = maxId + 1,
                CocktailName = model.CocktailName,
                Price = model.Price
            });
            int maxPCId = 0;
            for (int i = 0; i < source.ElementRequirements.Count; ++i)
            {
                if (source.ElementRequirements[i].ID > maxPCId)
                {
                    maxPCId = source.ElementRequirements[i].ID;
                }
            }
            for (int i = 0; i < model.ElementRequirements.Count; ++i)
            {
                for (int j = 1; j < model.ElementRequirements.Count; ++j)
                {
                    if (model.ElementRequirements[i].ElementID ==
                        model.ElementRequirements[j].ElementID)
                    {
                        model.ElementRequirements[i].Count +=
                            model.ElementRequirements[j].Count;
                        model.ElementRequirements.RemoveAt(j--);
                    }
                }
            }
            for (int i = 0; i < model.ElementRequirements.Count; ++i)
            {
                source.ElementRequirements.Add(new ElementRequirement
                {
                    ID = ++maxPCId,
                    CocktailID = maxId + 1,
                    ElementID = model.ElementRequirements[i].ElementID,
                    Count = model.ElementRequirements[i].Count
                });
            }
        }

        public void UpdElement(CocktailBindModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Cocktails.Count; ++i)
            {
                if (source.Cocktails[i].ID == model.ID)
                {
                    index = i;
                }
                if (source.Cocktails[i].CocktailName == model.CocktailName &&
                    source.Cocktails[i].ID != model.ID)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Cocktails[index].CocktailName = model.CocktailName;
            source.Cocktails[index].Price = model.Price;
            int maxPCId = 0;
            for (int i = 0; i < source.ElementRequirements.Count; ++i)
            {
                if (source.ElementRequirements[i].ID > maxPCId)
                {
                    maxPCId = source.ElementRequirements[i].ID;
                }
            }
            for (int i = 0; i < source.ElementRequirements.Count; ++i)
            {
                if (source.ElementRequirements[i].CocktailID == model.ID)
                {
                    bool flag = true;
                    for (int j = 0; j < model.ElementRequirements.Count; ++j)
                    {
                        if (source.ElementRequirements[i].ID == model.ElementRequirements[j].ID)
                        {
                            source.ElementRequirements[i].Count = model.ElementRequirements[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        source.ElementRequirements.RemoveAt(i--);
                    }
                }
            }
            for (int i = 0; i < model.ElementRequirements.Count; ++i)
            {
                if (model.ElementRequirements[i].ID == 0)
                {
                    for (int j = 0; j < source.ElementRequirements.Count; ++j)
                    {
                        if (source.ElementRequirements[j].CocktailID == model.ID &&
                            source.ElementRequirements[j].ElementID == model.ElementRequirements[i].ElementID)
                        {
                            source.ElementRequirements[j].Count += model.ElementRequirements[i].Count;
                            model.ElementRequirements[i].ID = source.ElementRequirements[j].ID;
                            break;
                        }
                    }
                    if (model.ElementRequirements[i].ID == 0)
                    {
                        source.ElementRequirements.Add(new ElementRequirement
                        {
                            ID = ++maxPCId,
                            CocktailID = model.ID,
                            ElementID = model.ElementRequirements[i].ElementID,
                            Count = model.ElementRequirements[i].Count
                        });
                    }
                }
            }
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.ElementRequirements.Count; ++i)
            {
                if (source.ElementRequirements[i].CocktailID == id)
                {
                    source.ElementRequirements.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Cocktails.Count; ++i)
            {
                if (source.Cocktails[i].ID == id)
                {
                    source.Cocktails.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}