using System;
using System.Collections.Generic;
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

        public List<ElementViewModel> GetList()
        {
            List<ElementViewModel> result = new List<ElementViewModel>();
            for (int i = 0; i < source.Elements.Count; ++i)
            {
                result.Add(new ElementViewModel
                {
                    ID = source.Elements[i].ID,
                    ElementName = source.Elements[i].ElementName
                });
            }
            return result;
        }

        public ElementViewModel GetElement(int ID)
        {
            for (int i = 0; i < source.Elements.Count; ++i)
            {
                if (source.Elements[i].ID == ID)
                {
                    return new ElementViewModel
                    {
                        ID = source.Elements[i].ID,
                        ElementName = source.Elements[i].ElementName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ElementBindModel model)
        {
            int maxID = 0;
            for (int i = 0; i < source.Elements.Count; ++i)
            {
                if (source.Elements[i].ID > maxID)
                {
                    maxID = source.Elements[i].ID;
                }
                if (source.Elements[i].ElementName == model.ElementName)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
            }
            source.Elements.Add(new Element
            {
                ID = maxID + 1,
                ElementName = model.ElementName
            });
        }

        public void UpdElement(ElementBindModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Elements.Count; ++i)
            {
                if (source.Elements[i].ID == model.ID)
                {
                    index = i;
                }
                if (source.Elements[i].ElementName == model.ElementName &&
                    source.Elements[i].ID != model.ID)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Elements[index].ElementName = model.ElementName;
        }

        public void DelElement(int ID)
        {
            for (int i = 0; i < source.Elements.Count; ++i)
            {
                if (source.Elements[i].ID == ID)
                {
                    source.Elements.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
