using BarService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AddElement(CocktailBindModel model)
        {
            Cocktail elem = source.Cocktails.FirstOrDefault(cocktail => cocktail.CocktailName == model.CocktailName);
            if (elem != null)
            {
                throw new Exception("Такой коктейль уже есть");
            }
            int maxId = source.Cocktails.Count > 0 ? source.Cocktails.Max(cocktail => cocktail.ID) : 0;
            source.Cocktails.Add(new Cocktail
            {
                ID = maxId + 1,
                CocktailName = model.CocktailName,
                Price = model.Price
            });
            int maxPCId = source.ElementRequirements.Count > 0 ? source.ElementRequirements.Max(cocktail => cocktail.ID) : 0;
            var groupElements = model.ElementRequirements.GroupBy(cocktail => cocktail.ElementID).Select(cocktail => new
            {
                ElementId = cocktail.Key,
                Count = cocktail.Sum(r => r.Count)
            });
            foreach (var groupElement in groupElements)
            {
                source.ElementRequirements.Add(new ElementRequirement
                {
                    ID = ++maxPCId,
                    CocktailID = maxId + 1,
                    ElementID = groupElement.ElementId,
                    Count = groupElement.Count
                });
            }
        }

        public void DelElement(int id)
        {
            Cocktail element = source.Cocktails.FirstOrDefault(cocktail => cocktail.ID == id);
            if (element != null)
            {
                source.ElementRequirements.RemoveAll(cocktail => cocktail.CocktailID == id);
                source.Cocktails.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public CocktailViewModel GetElement(int id)
        {
            Cocktail element = source.Cocktails.FirstOrDefault(cocktail => cocktail.ID == id);
            if (element != null)
            {
                return new CocktailViewModel
                {
                    ID = element.ID,
                    CocktailName = element.CocktailName,
                    Price = element.Price,
                    ElementRequirements = source.ElementRequirements.Where(cocktailPC => cocktailPC.CocktailID == element.ID).Select(cocktailPC => new ElementRequirementsViewModel
                    {
                        ID = cocktailPC.ID,
                        CocktailID = cocktailPC.CocktailID,
                        ElementID = cocktailPC.ElementID,
                        ElementName = source.Elements
                                        .FirstOrDefault(cocktailC => cocktailC.ID == cocktailPC.ElementID)?.ElementName,
                        Count = cocktailPC.Count
                    }).ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<CocktailViewModel> GetList()
        {
            List<CocktailViewModel> result = source.Cocktails
                .Select(cocktail => new CocktailViewModel
                {
                    ID = cocktail.ID,
                    CocktailName = cocktail.CocktailName,
                    Price = cocktail.Price,
                    ElementRequirements = source.ElementRequirements.Where(cocktailPC => cocktailPC.CocktailID == cocktail.ID).Select(cocktailPC => new ElementRequirementsViewModel
                    {
                        ID = cocktailPC.ID,
                        CocktailID = cocktailPC.CocktailID,
                        ElementID = cocktailPC.ElementID,
                        ElementName = source.Elements.FirstOrDefault(cocktailC => cocktailC.ID == cocktailPC.ElementID)?.ElementName,
                        Count = cocktailPC.Count
                    }).ToList()
                }).ToList();
            return result;
        }

        public void UpdElement(CocktailBindModel model)
        {
            Cocktail element = source.Cocktails.FirstOrDefault(cocktail => cocktail.CocktailName == model.CocktailName && cocktail.ID != model.ID);
            if (element != null)
            {
                throw new Exception("Такой коктейль уже есть");
            }
            element = source.Cocktails.FirstOrDefault(cocktail => cocktail.ID == model.ID);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CocktailName = model.CocktailName;
            element.Price = model.Price;
            int maxPCId = source.ElementRequirements.Count > 0 ? source.ElementRequirements.Max(cocktail => cocktail.ID) : 0;
            var compIds = model.ElementRequirements.Select(cocktail => cocktail.ElementID).Distinct();
            var updateElements = source.ElementRequirements.Where(cocktail => cocktail.CocktailID == model.ID && compIds.Contains(cocktail.ElementID));
            foreach (var updateElement in updateElements)
            {
                updateElement.Count = model.ElementRequirements.FirstOrDefault(cocktail => cocktail.ID == updateElement.ID).Count;
            }
            source.ElementRequirements.RemoveAll(cocktail => cocktail.CocktailID == model.ID && !compIds.Contains(cocktail.ElementID));
            var groupElements = model.ElementRequirements.Where(cocktail => cocktail.ID == 0).GroupBy(cocktail => cocktail.ElementID).Select(cocktail => new
            {
                ElementID = cocktail.Key,
                Count = cocktail.Sum(r => r.Count)
            });
            foreach (var groupElement in groupElements)
            {
                ElementRequirement elementPC = source.ElementRequirements.FirstOrDefault(cocktail => cocktail.CocktailID == model.ID && cocktail.ElementID == groupElement.ElementID);
                if (elementPC != null)
                {
                    elementPC.Count += groupElement.Count;
                }
                else
                {
                    source.ElementRequirements.Add(new ElementRequirement
                    {
                        ID = ++maxPCId,
                        CocktailID = model.ID,
                        ElementID = groupElement.ElementID,
                        Count = groupElement.Count
                    });
                }
            }
        }
    }
}