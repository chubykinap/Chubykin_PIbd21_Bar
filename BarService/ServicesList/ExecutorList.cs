using BarService.Interfaces;
using System;
using System.Collections.Generic;
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

        public List<ExecutorViewModel> GetList()
        {
            List<ExecutorViewModel> result = new List<ExecutorViewModel>();
            for (int i = 0; i < source.Executors.Count; ++i)
            {
                result.Add(new ExecutorViewModel
                {
                    ID = source.Executors[i].ID,
                    ExecutorFIO = source.Executors[i].ExecutorFIO
                });
            }
            return result;
        }

        public ExecutorViewModel GetElement(int ID)
        {
            for (int i = 0; i < source.Executors.Count; ++i)
            {
                if (source.Executors[i].ID == ID)
                {
                    return new ExecutorViewModel
                    {
                        ID = source.Executors[i].ID,
                        ExecutorFIO = source.Executors[i].ExecutorFIO
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ExecutorBindModel model)
        {
            int maxID = 0;
            for (int i = 0; i < source.Executors.Count; ++i)
            {
                if (source.Executors[i].ID > maxID)
                {
                    maxID = source.Executors[i].ID;
                }
                if (source.Executors[i].ExecutorFIO == model.ExecutorFIO)
                {
                    throw new Exception("Уже есть сотрудник с таким ФИО");
                }
            }
            source.Executors.Add(new Executor
            {
                ID = maxID + 1,
                ExecutorFIO = model.ExecutorFIO
            });
        }

        public void UpdElement(ExecutorBindModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Executors.Count; ++i)
            {
                if (source.Executors[i].ID == model.ID)
                {
                    index = i;
                }
                if (source.Executors[i].ExecutorFIO == model.ExecutorFIO &&
                    source.Executors[i].ID != model.ID)
                {
                    throw new Exception("Уже есть сотрудник с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Executors[index].ExecutorFIO = model.ExecutorFIO;
        }

        public void DelElement(int ID)
        {
            for (int i = 0; i < source.Executors.Count; ++i)
            {
                if (source.Executors[i].ID == ID)
                {
                    source.Executors.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
