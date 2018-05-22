using BarModel;
using BarService.BindingModels;
using BarService.Interfaces;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Data.Entity;

namespace BarService.BDImplementation
{
    public class MainBD : IMainService
    {
        private BarDBContext context;

        public MainBD(BarDBContext context)
        {
            this.context = context;
        }

        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = context.Orders
                .Select(rec => new OrderViewModel
                {
                    ID = rec.ID,
                    CustomerID = rec.CustomerID,
                    CocktailID = rec.CocktailID,
                    ExecutorID = rec.ExecutorID,
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                                SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                                SqlFunctions.DateName("yyyy", rec.DateCreate),
                    DateImplement = rec.DateImplement == null ? "" :
                                        SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " +
                                        SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " +
                                        SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                    Status = rec.Status.ToString(),
                    Count = rec.Count,
                    Sum = rec.Sum,
                    CustomerFIO = rec.Customer.CustomerFIO,
                    CocktailName = rec.Cocktail.CocktailName,
                    ExecutorName = rec.Executor.ExecutorFIO
                })
                .ToList();
            return result;
        }

        public void CreateOrder(OrderBindModel model)
        {
            context.Orders.Add(new Order
            {
                CustomerID = model.CustomerID,
                CocktailID = model.CocktailID,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderStatus.Принят
            });
            context.SaveChanges();
        }

        public void TakeOrderInWork(OrderBindModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Order element = context.Orders.FirstOrDefault(rec => rec.ID == model.ID);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    var CocktailElements = context.ElementRequirements
                                                .Include(rec => rec.Element)
                                                .Where(rec => rec.CocktailID == element.CocktailID);
                    foreach (var CocktailElement in CocktailElements)
                    {
                        int countOnStorages = CocktailElement.Count * element.Count;
                        var StorageElements = context.ElementRequirements
                                                    .Where(rec => rec.ElementID == CocktailElement.ElementID);
                        foreach (var StorageElement in StorageElements)
                        {
                            if (StorageElement.Count >= countOnStorages)
                            {
                                StorageElement.Count -= countOnStorages;
                                countOnStorages = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStorages -= StorageElement.Count;
                                StorageElement.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStorages > 0)
                        {
                            throw new Exception("Не достаточно компонента " +
                                CocktailElement.Element.ElementName + " требуется " +
                                CocktailElement.Count + ", не хватает " + countOnStorages);
                        }
                    }
                    element.ExecutorID = model.ExecutorID;
                    element.DateImplement = DateTime.Now;
                    element.Status = OrderStatus.Выполняется;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void FinishOrder(int ID)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.ID == ID);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = OrderStatus.Готов;
            context.SaveChanges();
        }

        public void PayOrder(int ID)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.ID == ID);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = OrderStatus.Оплачен;
            context.SaveChanges();
        }

        public void PutElementOnStorage(ElementStorageBindModel model)
        {
            ElementStorage element = context.ElementStorages
                                                .FirstOrDefault(rec => rec.StorageID == model.StorageID &&
                                                                    rec.ElementID == model.ElementID);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                context.ElementStorages.Add(new ElementStorage
                {
                    StorageID = model.StorageID,
                    ElementID = model.ElementID,
                    Count = model.Count
                });
            }
            context.SaveChanges();
        }
    }
}
