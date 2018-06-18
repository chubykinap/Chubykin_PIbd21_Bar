using BarService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using BarService.BindingModels;
using BarService.ViewModel;
using BarModel;

namespace BarService.ServicesList
{
    public class CustomerList : ICustomer
    {
        private DataListSingleton source;

        public CustomerList()
        {
            source = DataListSingleton.GetInstance();
        }

        public void AddElement(CustomerBindModel model)
        {
            Customer elem = source.Customers.FirstOrDefault(cust => cust.CustomerFIO == model.CustomerFIO);
            if (elem != null)
            {
                throw new Exception("Такой покупатель уже есть");
            }
            int maxId = source.Customers.Count > 0 ? source.Customers.Max(cust => cust.ID) : 0;
            source.Customers.Add(new Customer
            {
                ID = maxId + 1,
                CustomerFIO = model.CustomerFIO
            });
        }

        public void DelElement(int id)
        {
            Customer elem = source.Customers.FirstOrDefault(cust => cust.ID == id);
            if (elem != null)
            {
                source.Customers.Remove(elem);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public CustomerViewModel GetElement(int id)
        {
            Customer elem = source.Customers.FirstOrDefault(cust => cust.ID == id);
            if (elem != null)
            {
                return new CustomerViewModel
                {
                    ID = elem.ID,
                    CustomerFIO = elem.CustomerFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<CustomerViewModel> GetList()
        {
            List<CustomerViewModel> result = source.Customers.Select(cust => new CustomerViewModel
            {
                ID = cust.ID,
                CustomerFIO = cust.CustomerFIO
            }).ToList();
            return result;
        }

        public void UpdElement(CustomerBindModel model)
        {
            Customer elem = source.Customers.FirstOrDefault(cust => cust.CustomerFIO == model.CustomerFIO && cust.ID == model.ID);
            if (elem != null)
            {
                throw new Exception("Такой покупатель уже есть");
            }
            elem = source.Customers.FirstOrDefault(cust => cust.ID == model.ID);
            if (elem == null)
            {
                throw new Exception("Элемент не найден");
            }
            elem.CustomerFIO = model.CustomerFIO;
        }
    }
}
