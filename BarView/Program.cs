using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
using BarService.Interfaces;
using BarService.ServicesList;

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
            currentContainer.RegisterType<ICustomer, CustomerList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IElement, ElementList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExecutor, ExecutorList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICocktail, CocktailList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorage, StorageList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainList>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
