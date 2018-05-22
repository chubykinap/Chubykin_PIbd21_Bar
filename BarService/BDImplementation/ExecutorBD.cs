using BarModel;
using BarService.BindingModels;
using BarService.Interfaces;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarService.BDImplementation
{
    public class ExecutorBD : IExecutor
    {
        private BarDBContext context;

        public ExecutorBD(BarDBContext context)
        {
            this.context = context;
        }

        public List<ExecutorViewModel> GetList()
        {
            List<ExecutorViewModel> result = context.Executors
                .Select(rec => new ExecutorViewModel
                {
                    ID = rec.ID,
                    ExecutorFIO = rec.ExecutorFIO
                })
                .ToList();
            return result;
        }

        public ExecutorViewModel GetElement(int id)
        {
            Executor element = context.Executors.FirstOrDefault(rec => rec.ID == id);
            if (element != null)
            {
                return new ExecutorViewModel
                {
                    ID = element.ID,
                    ExecutorFIO = element.ExecutorFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ExecutorBindModel model)
        {
            Executor element = context.Executors.FirstOrDefault(rec => rec.ExecutorFIO == model.ExecutorFIO);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            context.Executors.Add(new Executor
            {
                ExecutorFIO = model.ExecutorFIO
            });
            context.SaveChanges();
        }

        public void UpdElement(ExecutorBindModel model)
        {
            Executor element = context.Executors.FirstOrDefault(rec =>
                                        rec.ExecutorFIO == model.ExecutorFIO && rec.ID != model.ID);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            element = context.Executors.FirstOrDefault(rec => rec.ID == model.ID);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ExecutorFIO = model.ExecutorFIO;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Executor element = context.Executors.FirstOrDefault(rec => rec.ID == id);
            if (element != null)
            {
                context.Executors.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
