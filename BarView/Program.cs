using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
using BarService.Interfaces;
using System.Data.Entity;
using BarService.BDImplementation;
using BarService;

namespace BarView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<MainForm>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, BarDBContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICustomer, CustomerBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IElement, ElementBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExecutor, ExecutorBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICocktail, CocktailBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorage, StorageBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainBD>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
