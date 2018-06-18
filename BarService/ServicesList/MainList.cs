using System;
using System.Collections.Generic;
using System.Linq;
using BarService.BindingModels;
using BarService.Interfaces;
using BarService.ViewModel;
using BarModel;

namespace BarService.ServicesList
{
    public class MainList : IMainService
    {
        private DataListSingleton source;

        public MainList()
        {
            source = DataListSingleton.GetInstance();
        }

        public void CreateOrder(OrderBindModel model)
        {
            int maxId = source.Orders.Count > 0 ? source.Orders.Max(order => order.ID) : 0;
            source.Orders.Add(new Order
            {
                ID = maxId + 1,
                CustomerID = model.CustomerID,
                CocktailID = model.CocktailID,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderStatus.Принят
            });
        }

        public void FinishOrder(int id)
        {
            Order elem = source.Orders.FirstOrDefault(order => order.ID == id);
            if (elem == null)
            {
                throw new Exception("Элемент не найден");
            }
            elem.Status = OrderStatus.Готов;
        }

        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = source.Orders.Select(order => new OrderViewModel
            {
                ID = order.ID,
                CustomerID = order.CustomerID,
                CocktailID = order.CocktailID,
                ExecutorID = order.ExecutorID,
                DateCreate = order.DateCreate.ToLongDateString(),
                DateImplement = order.DateImplement?.ToLongDateString(),
                Status = order.Status.ToString(),
                Count = order.Count,
                Sum = order.Sum,
                CustomerFIO = source.Customers.FirstOrDefault(orderC => orderC.ID == order.CustomerID)?.CustomerFIO,
                CocktailName = source.Elements.FirstOrDefault(orderP => orderP.ID == order.CocktailID)?.ElementName,
                ExecutorName = source.Executors.FirstOrDefault(orderI => orderI.ID == order.ExecutorID)?.ExecutorFIO
            }).ToList();
            return result;
        }

        public void PayOrder(int id)
        {
            Order elem = source.Orders.FirstOrDefault(order => order.ID == id);
            if (elem == null)
            {
                throw new Exception("Элемент не найден");
            }
            elem.Status = OrderStatus.Оплачен;
        }

        public void PutElementOnStorage(ElementStorageBindModel model)
        {
            ElementStorage element = source.ElementStorages.FirstOrDefault(order => order.StorageID == model.StorageID && order.ElementID == model.ElementID);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                int maxId = source.ElementStorages.Count > 0 ? source.ElementStorages.Max(order => order.ID) : 0;
                source.ElementStorages.Add(new ElementStorage
                {
                    StorageID = model.StorageID,
                    ElementID = model.ElementID,
                    Count = model.Count
                });
            }
        }

        public void TakeOrderInWork(OrderBindModel model)
        {
            Order element = source.Orders.FirstOrDefault(order => order.ID == model.ID);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            var CocktailElements = source.ElementRequirements.Where(order => order.ElementID == element.CocktailID);
            foreach (var CocktailElement in CocktailElements)
            {
                int countOnStorages = source.ElementStorages.Where(order => order.ElementID == CocktailElement.ElementID).Sum(order => order.Count);
                if (countOnStorages < CocktailElement.Count * element.Count)
                {
                    var ElementName = source.Elements.FirstOrDefault(order => order.ID == CocktailElement.ElementID);
                    throw new Exception("Не достаточно компонента " + ElementName?.ElementName +
                        " требуется " + CocktailElement.Count * element.Count + ", в наличии " + countOnStorages);
                }
            }
            foreach (var CocktailElement in CocktailElements)
            {
                int countOnStorages = CocktailElement.Count * element.Count;
                var StorageElements = source.ElementStorages.Where(order => order.ElementID == CocktailElement.ElementID);
                foreach (var StorageElement in StorageElements)
                {
                    if (StorageElement.Count >= countOnStorages)
                    {
                        StorageElement.Count -= countOnStorages;
                        break;
                    }
                    else
                    {
                        countOnStorages -= StorageElement.Count;
                        StorageElement.Count = 0;
                    }
                }
            }
            element.ExecutorID = model.ExecutorID;
            element.DateImplement = DateTime.Now;
            element.Status = OrderStatus.Выполняется;
        }
    }
}

