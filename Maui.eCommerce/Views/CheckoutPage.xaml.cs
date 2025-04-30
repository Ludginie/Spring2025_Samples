using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;
using Microsoft.Maui.Controls;
using System;

namespace Maui.eCommerce.Views
{
    public partial class CheckoutPage : ContentPage
    {
        private CheckoutViewModel viewModel;

        public CheckoutPage()
        {
            InitializeComponent();
            viewModel = BindingContext as CheckoutViewModel ?? new CheckoutViewModel();
        }

       

    }
}