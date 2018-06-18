using BarService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using BarService.BindingModels;
using BarService.ViewModel;
using BarModel;

namespace BarService.ServicesList
{
    public class ExecutorList : IExecutor
    {
        private DataListSingleton source;

        public ExecutorList()
        {
            source = DataListSingleton.GetInstance();
        }

        public void AddElement(ExecutorBindModel model)
        {
            Executor elem = source.Executors.FirstOrDefault(executor => executor.ExecutorFIO == model.ExecutorFIO);
            if (elem != null)
            {
                throw new Exception("Такой исполнитель уже есть");
            }
            int maxId = source.Executors.Count > 0 ? source.Executors.Max(executor => executor.ID) : 0;
            source.Executors.Add(new Executor
            {
                ID = maxId + 1,
                ExecutorFIO = model.ExecutorFIO
            });
        }

        public void DelElement(int id)
        {
            Executor elem = source.Executors.FirstOrDefault(executor => executor.ID == id);
            if (elem != null)
            {
                source.Executors.Remove(elem);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public ExecutorViewModel GetElement(int id)
        {
            Executor elem = source.Executors.FirstOrDefault(executor => executor.ID == id);
            if (elem != null)
            {
                return new ExecutorViewModel
                {
                    ID = elem.ID,
                    ExecutorFIO = elem.ExecutorFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<ExecutorViewModel> GetList()
        {
            List<ExecutorViewModel> result = source.Executors.Select(rec => new ExecutorViewModel
            {
                ID = rec.ID,
                ExecutorFIO = rec.ExecutorFIO
            }).ToList();
            return result;
        }

        public void UpdElement(ExecutorBindModel model)
        {
            Executor elem = source.Executors.FirstOrDefault(executor => executor.ExecutorFIO == model.ExecutorFIO && executor.ID == model.ID);
            if (elem != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            elem = source.Executors.FirstOrDefault(executor => executor.ID == model.ID);
            if (elem == null)
            {
                throw new Exception("Элемент не найден");
            }
            elem.ExecutorFIO = model.ExecutorFIO;
        }
    }
}
