using BarModel;
using BarService.BindingModels;
using BarService.Interfaces;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarService.BDImplementation
{
    public class StorageBD : IStorage
    {
        private BarDBContext context;

        public StorageBD(BarDBContext context)
        {
            this.context = context;
        }

        public StorageBD()
        {
            this.context = new BarDBContext();
        }

        public List<StorageViewModel> GetList()
        {
            List<StorageViewModel> result = context.Storages
                .Select(rec => new StorageViewModel
                {
                    ID = rec.ID,
                    StorageName = rec.StorageName,
                    StorageElements = context.ElementStorages
                            .Where(recPC => recPC.StorageID == rec.ID)
                            .Select(recPC => new ElementStorageViewModel
                            {
                                ID = recPC.ID,
                                StorageID = recPC.StorageID,
                                ElementID = recPC.ElementID,
                                ElementName = recPC.Element.ElementName,
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public StorageViewModel GetElement(int id)
        {
            Storage element = context.Storages.FirstOrDefault(rec => rec.ID == id);
            if (element != null)
            {
                return new StorageViewModel
                {
                    ID = element.ID,
                    StorageName = element.StorageName,
                    StorageElements = context.ElementStorages
                            .Where(recPC => recPC.StorageID == element.ID)
                            .Select(recPC => new ElementStorageViewModel
                            {
                                ID = recPC.ID,
                                StorageID = recPC.StorageID,
                                ElementID = recPC.ElementID,
                                ElementName = recPC.Element.ElementName,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(StorageBindModel model)
        {
            Storage element = context.Storages.FirstOrDefault(rec => rec.StorageName == model.StorageName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            context.Storages.Add(new Storage
            {
                StorageName = model.StorageName
            });
            context.SaveChanges();
        }

        public void UpdElement(StorageBindModel model)
        {
            Storage element = context.Storages.FirstOrDefault(rec =>
                                        rec.StorageName == model.StorageName && rec.ID != model.ID);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = context.Storages.FirstOrDefault(rec => rec.ID == model.ID);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.StorageName = model.StorageName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Storage element = context.Storages.FirstOrDefault(rec => rec.ID == id);
                    if (element != null)
                    {
                        context.ElementStorages.RemoveRange(
                                            context.ElementStorages.Where(rec => rec.StorageID == id));
                        context.Storages.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
