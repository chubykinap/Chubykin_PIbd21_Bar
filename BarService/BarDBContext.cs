using BarModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace BarService
{
    public class BarDBContext : DbContext
    {
        public BarDBContext() : base("NewBarDatabase")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Element> Elements { get; set; }

        public virtual DbSet<Executor> Executors { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Cocktail> Cocktails { get; set; }

        public virtual DbSet<ElementRequirement> ElementRequirements { get; set; }

        public virtual DbSet<Storage> Storages { get; set; }

        public virtual DbSet<ElementStorage> ElementStorages { get; set; }

        public virtual DbSet<MessageInfo> MessageInfos { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception)
            {
                foreach (var entry in ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.Reload();
                            break;
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                    }
                }
                throw;
            }
        }
    }
}
