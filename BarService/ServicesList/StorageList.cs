using BarService.Interfaces;
using System;
using System.Collections.Generic;
using BarService.ViewModel;
using BarModel;
using BarService.BindingModels;

namespace BarService.ServicesList
{
    public class StorageList : IStorage
    {
        private DataListSingleton source;

        public StorageList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<StorageViewModel> GetList()
        {
            List<StorageViewModel> result = new List<StorageViewModel>();
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                List<ElementStorageViewModel> ElementStorages = new List<ElementStorageViewModel>();
                for (int j = 0; j < source.ElementStorages.Count; ++j)
                {
                    if (source.ElementStorages[j].StorageID == source.Storages[i].Id)
                    {
                        string ElementName = string.Empty;
                        for (int k = 0; k < source.Elements.Count; ++k)
                        {
                            if (source.ElementRequirements[j].ElementID == source.Elements[k].ID)
                            {
                                ElementName = source.Elements[k].ElementName;
                                break;
                            }
                        }
                        ElementStorages.Add(new ElementStorageViewModel
                        {
                            ID = source.ElementStorages[j].ID,
                            StorageID = source.ElementStorages[j].StorageID,
                            ElementID = source.ElementStorages[j].ElementID,
                            ElementName = ElementName,
                            Count = source.ElementStorages[j].Count
                        });
                    }
                }
                result.Add(new StorageViewModel
                {
                    ID = source.Storages[i].Id,
                    StorageName = source.Storages[i].StorageName,
                    StorageElements = ElementStorages
                });
            }
            return result;
        }

        public StorageViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                List<ElementStorageViewModel> ElementStorages = new List<ElementStorageViewModel>();
                for (int j = 0; j < source.ElementStorages.Count; ++j)
                {
                    if (source.ElementStorages[j].StorageID == source.Storages[i].Id)
                    {
                        string ElementName = string.Empty;
                        for (int k = 0; k < source.Elements.Count; ++k)
                        {
                            if (source.ElementRequirements[j].ElementID == source.Elements[k].ID)
                            {
                                ElementName = source.Elements[k].ElementName;
                                break;
                            }
                        }
                        ElementStorages.Add(new ElementStorageViewModel
                        {
                            ID = source.ElementStorages[j].ID,
                            StorageID = source.ElementStorages[j].StorageID,
                            ElementID = source.ElementStorages[j].ElementID,
                            ElementName = ElementName,
                            Count = source.ElementStorages[j].Count
                        });
                    }
                }
                if (source.Storages[i].Id == id)
                {
                    return new StorageViewModel
                    {
                        ID = source.Storages[i].Id,
                        StorageName = source.Storages[i].StorageName,
                        StorageElements = ElementStorages
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(StorageBindModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id > maxId)
                {
                    maxId = source.Storages[i].Id;
                }
                if (source.Storages[i].StorageName == model.StorageName)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            source.Storages.Add(new Storage
            {
                Id = maxId + 1,
                StorageName = model.StorageName
            });
        }

        public void UpdElement(StorageBindModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == model.ID)
                {
                    index = i;
                }
                if (source.Storages[i].StorageName == model.StorageName &&
                    source.Storages[i].Id != model.ID)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Storages[index].StorageName = model.StorageName;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.ElementStorages.Count; ++i)
            {
                if (source.ElementStorages[i].StorageID == id)
                {
                    source.ElementStorages.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == id)
                {
                    source.Storages.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}