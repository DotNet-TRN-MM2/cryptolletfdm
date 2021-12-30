using Autofac;
using cryptolletfdm.Modules.AddTransaction;
using Xamarin.Forms;

namespace cryptolletfdm
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<AppShellViewModel>();

            Routing.RegisterRoute("AddTransactionViewModel", typeof(AddTransactionView));
        }

    }
}
