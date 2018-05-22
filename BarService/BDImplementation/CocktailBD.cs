using BarModel;
using BarService.BindingModels;
using BarService.Interfaces;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarService.BDImplementation
{
    public class CocktailBD : ICocktail
    {
        private BarDBContext context;

        public CocktailBD(BarDBContext context)
        {
            this.context = context;
        }

        public List<CocktailViewModel> GetList()
        {
            List<CocktailViewModel> result = context.Cocktails
                .Select(rec => new CocktailViewModel
                {
                    ID = rec.ID,
                    CocktailName = rec.CocktailName,
                    Price = rec.Price,
                    ElementRequirements = context.ElementRequirements.Where(recPC => recPC.CocktailID == rec.ID)
                            .Select(recPC => new ElementRequirementsViewModel
                            {
                                ID = recPC.ID,
                                CocktailID = recPC.CocktailID,
                                ElementID = recPC.ElementID,
                                ElementName = recPC.Element.ElementName,
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public CocktailViewModel GetElement(int ID)
        {
            Cocktail element = context.Cocktails.FirstOrDefault(rec => rec.ID == ID);
            if (element != null)
            {
                return new CocktailViewModel
                {
                    ID = element.ID,
                    CocktailName = element.CocktailName,
                    Price = element.Price,
                    ElementRequirements = context.ElementRequirements
                            .Where(recPC => recPC.CocktailID == element.ID)
                            .Select(recPC => new ElementRequirementsViewModel
                            {
                                ID = recPC.ID,
                                CocktailID = recPC.CocktailID,
                                ElementID = recPC.ElementID,
                                ElementName = recPC.Element.ElementName,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CocktailBindModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Cocktail element = context.Cocktails.FirstOrDefault(rec => rec.CocktailName == model.CocktailName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new Cocktail
                    {
                        CocktailName = model.CocktailName,
                        Price = model.Price
                    };
                    context.Cocktails.Add(element);
                    context.SaveChanges();
                    var groupElements = model.ElementRequirements
                                                .GroupBy(rec => rec.ElementID)
                                                .Select(rec => new
                                                {
                                                    ElementID = rec.Key,
                                                    Count = rec.Sum(r => r.Count)
                                                });
                    foreach (var groupElement in groupElements)
                    {
                        context.ElementRequirements.Add(new ElementRequirement
                        {
                            CocktailID = element.ID,
                            ElementID = groupElement.ElementID,
                            Count = groupElement.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdElement(CocktailBindModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Cocktail element = context.Cocktails.FirstOrDefault(rec =>
                                        rec.CocktailName == model.CocktailName && rec.ID != model.ID);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Cocktails.FirstOrDefault(rec => rec.ID == model.ID);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.CocktailName = model.CocktailName;
                    element.Price = model.Price;
                    context.SaveChanges();

                    var compIDs = model.ElementRequirements.Select(rec => rec.ElementID).Distinct();
                    var updateElements = context.ElementRequirements
                                                    .Where(rec => rec.CocktailID == model.ID &&
                                                        compIDs.Contains(rec.ElementID));
                    foreach (var updateElement in updateElements)
                    {
                        updateElement.Count = model.ElementRequirements
                                                        .FirstOrDefault(rec => rec.ID == updateElement.ID).Count;
                    }
                    context.SaveChanges();
                    context.ElementRequirements.RemoveRange(
                                        context.ElementRequirements.Where(rec => rec.CocktailID == model.ID &&
                                                                            !compIDs.Contains(rec.ElementID)));
                    context.SaveChanges();
                    var groupElements = model.ElementRequirements
                                                .Where(rec => rec.ID == 0)
                                                .GroupBy(rec => rec.ElementID)
                                                .Select(rec => new
                                                {
                                                    ElementID = rec.Key,
                                                    Count = rec.Sum(r => r.Count)
                                                });
                    foreach (var groupElement in groupElements)
                    {
                        ElementRequirement elementPC = context.ElementRequirements
                                                .FirstOrDefault(rec => rec.CocktailID == model.ID &&
                                                                rec.ElementID == groupElement.ElementID);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupElement.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.ElementRequirements.Add(new ElementRequirement
                            {
                                CocktailID = model.ID,
                                ElementID = groupElement.ElementID,
                                Count = groupElement.Count
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DelElement(int ID)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Cocktail element = context.Cocktails.FirstOrDefault(rec => rec.ID == ID);
                    if (element != null)
                    {
                        context.ElementRequirements.RemoveRange(
                                            context.ElementRequirements.Where(rec => rec.CocktailID == ID));
                        context.Cocktails.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
