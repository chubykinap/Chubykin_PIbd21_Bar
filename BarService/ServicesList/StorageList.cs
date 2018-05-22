using BarService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using BarService.BindingModels;
using BarService.ViewModel;
using BarModel;

namespace BarService.ServicesList
{
    public class StorageList : IStorage
    {
        private DataListSingleton source;

        public StorageList()
        {
            source = DataListSingleton.GetInstance();
        }

        public void AddElement(StorageBindModel model)
        {
            Storage elem = source.Storages.FirstOrDefault(storage => storage.StorageName == model.StorageName);
            if (elem != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.Storages.Count > 0 ? source.Storages.Max(storage => storage.ID) : 0;
            source.Storages.Add(new Storage
            {
                ID = maxId + 1,
                StorageName = model.StorageName
            });
        }

        public void DelElement(int id)
        {
            Storage element = source.Storages.FirstOrDefault(storage => storage.ID == id);
            if (element != null)
            {
                source.ElementStorages.RemoveAll(storage => storage.StorageID == id);
                source.Storages.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public StorageViewModel GetElement(int id)
        {
            Storage element = source.Storages.FirstOrDefault(storage => storage.ID == id);
            if (element != null)
            {
                return new StorageViewModel
                {
                    ID = element.ID,
                    StorageName = element.StorageName,
                    StorageElements = source.ElementStorages.Where(storagePC => storagePC.StorageID == element.ID).Select(storagePC => new ElementStorageViewModel
                    {
                        ID = storagePC.ID,
                        StorageID = storagePC.StorageID,
                        ElementID = storagePC.ElementID,
                        ElementName = source.Elements.FirstOrDefault(storageC => storageC.ID == storagePC.ElementID)?.ElementName,
                        Count = storagePC.Count
                    }).ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<StorageViewModel> GetList()
        {
            List<StorageViewModel> result = source.Storages.Select(storage => new StorageViewModel
            {
                ID = storage.ID,
                StorageName = storage.StorageName,
                StorageElements = source.ElementStorages.Where(storagePC => storagePC.StorageID == storage.ID).Select(storagePC => new ElementStorageViewModel
                {
                    ID = storagePC.ID,
                    StorageID = storagePC.StorageID,
                    ElementID = storagePC.ElementID,
                    ElementName = source.Elements.FirstOrDefault(storageC => storageC.ID == storagePC.ElementID)?.ElementName,
                    Count = storagePC.Count
                }).ToList()
            }).ToList();
            return result;
        }

        public void UpdElement(StorageBindModel model)
        {
            Storage elem = source.Storages.FirstOrDefault(storage => storage.StorageName == model.StorageName && storage.ID != model.ID);
            if (elem != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            elem = source.Storages.FirstOrDefault(storage => storage.ID == model.ID);
            if (elem == null)
            {
                throw new Exception("Элемент не найден");
            }
            elem.StorageName = model.StorageName;
        }
    }
}