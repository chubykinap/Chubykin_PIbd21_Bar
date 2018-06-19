using BarModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace BarService
{
    [Table("BarWebDatabase")]
    public class BarDBContext : DbContext
    {
        public BarDBContext()
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
    }
}
