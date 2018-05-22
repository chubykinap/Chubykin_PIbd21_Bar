using System;
using System.Collections.Generic;
using System.Linq;
using BarService.BindingModels;
using BarService.Interfaces;
using BarService.ViewModel;
using BarModel;

namespace BarService.ServicesList
{
    public class ElementList : IElement
    {
        private DataListSingleton source;

        public ElementList()
        {
            source = DataListSingleton.GetInstance();
        }

        public void AddElement(ElementBindModel model)
        {
            Element elem = source.Elements.FirstOrDefault(element => element.ElementName == model.ElementName);
            if (elem != null)
            {
                throw new Exception("Такой элемент уже есть");
            }
            int maxID = source.Elements.Count > 0 ? source.Elements.Max(element => element.ID) : 0;
            source.Elements.Add(new Element
            {
                ID = maxID + 1,
                ElementName = model.ElementName
            });
        }

        public void DelElement(int ID)
        {
            Element elem = source.Elements.FirstOrDefault(element => element.ID == ID);
            if (elem != null)
            {
                source.Elements.Remove(elem);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public ElementViewModel GetElement(int ID)
        {
            Element elem = source.Elements.FirstOrDefault(element => element.ID == ID);
            if (elem != null)
            {
                return new ElementViewModel
                {
                    ID = elem.ID,
                    ElementName = elem.ElementName
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<ElementViewModel> GetList()
        {
            List<ElementViewModel> result = source.Elements
                .Select(element => new ElementViewModel
                {
                    ID = element.ID,
                    ElementName = element.ElementName
                })
                .ToList();
            return result;
        }

        public void UpdElement(ElementBindModel model)
        {
            Element elem = source.Elements.FirstOrDefault(element =>
                                        element.ElementName == model.ElementName && element.ID != model.ID);
            if (elem != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            elem = source.Elements.FirstOrDefault(element => element.ID == model.ID);
            if (elem == null)
            {
                throw new Exception("Элемент не найден");
            }
            elem.ElementName = model.ElementName;
        }
    }
}
