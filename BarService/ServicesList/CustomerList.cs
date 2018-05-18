using BarService.Interfaces;
using System;
using System.Collections.Generic;
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

        public List<CustomerViewModel> GetList()
        {
            List<CustomerViewModel> result = new List<CustomerViewModel>();
            for (int i = 0; i < source.Customers.Count; ++i)
            {
                result.Add(new CustomerViewModel
                {
                    ID = source.Customers[i].ID,
                    CustomerFIO = source.Customers[i].CustomerFIO
                });
            }
            return result;
        }

        public CustomerViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Customers.Count; ++i)
            {
                if (source.Customers[i].ID == id)
                {
                    return new CustomerViewModel
                    {
                        ID = source.Customers[i].ID,
                        CustomerFIO = source.Customers[i].CustomerFIO
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CustomerBindModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Customers.Count; ++i)
            {
                if (source.Customers[i].ID > maxId)
                {
                    maxId = source.Customers[i].ID;
                }
                if (source.Customers[i].CustomerFIO == model.CustomerFIO)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            source.Customers.Add(new Customer
            {
                ID = maxId + 1,
                CustomerFIO = model.CustomerFIO
            });
        }

        public void UpdElement(CustomerBindModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Customers.Count; ++i)
            {
                if (source.Customers[i].ID == model.ID)
                {
                    index = i;
                }
                if (source.Customers[i].CustomerFIO == model.CustomerFIO &&
                    source.Customers[i].ID != model.ID)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Customers[index].CustomerFIO = model.CustomerFIO;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Customers.Count; ++i)
            {
                if (source.Customers[i].ID == id)
                {
                    source.Customers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
