namespace Maui.eCommerce
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("checkout", typeof(Views.CheckoutPage));
        }
    }
}

