using System.Reflection;
using Autofac;
using cryptolletfdm.Common.Models;
using cryptolletfdm.Common.Database;
using Xamarin.Forms;
using cryptolletfdm.Modules.Loading;

namespace cryptolletfdm
{
    public partial class App : Xamarin.Forms.Application
    {
        public static IContainer Container;

        public App()
        {
            InitializeComponent();
            //class used for registration
            var builder = new ContainerBuilder();
            //scan and register all classes in the assembly
            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess)
                   .AsImplementedInterfaces()
                   .AsSelf();
            // Keeping the line below so I can remember that I an use this User instead of transaction
            builder.RegisterType<Repository<User>>().As<IRepository<User>>();
            builder.RegisterType<Repository<Transaction>>().As<IRepository<Transaction>>();

            //get container
            Container = builder.Build();
            //set first page
            MainPage = Container.Resolve<LoadingView>();
        }
    }
}

