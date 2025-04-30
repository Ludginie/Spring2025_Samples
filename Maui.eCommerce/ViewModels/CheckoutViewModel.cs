using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels;

public class CheckoutViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Item> CartItems { get; set; } = new();
    public event PropertyChangedEventHandler? PropertyChanged;

    public CheckoutViewModel()
    {
        RefreshCart();
    }

    public double Subtotal => CartItems.Sum(item => (item.Product?.Price ?? 0) * (item.Quantity ?? 0));
    public double Tax => Subtotal * 0.07;
    public double Total => Subtotal + Tax;

    public string SubtotalFormatted => $"Subtotal: ${Subtotal:F2}";
    public string TaxFormatted => $"Tax (7%): ${Tax:F2}";
    public string TotalFormatted => $"Total: ${Total:F2}";

    public void RefreshCart()
    {
        CartItems.Clear();
        foreach (var item in ShoppingCartService.Current.CartItems)
        {
            CartItems.Add(item);
        }

        OnPropertyChanged(nameof(Subtotal));
        OnPropertyChanged(nameof(Tax));
        OnPropertyChanged(nameof(Total));
        OnPropertyChanged(nameof(SubtotalFormatted));
        OnPropertyChanged(nameof(TaxFormatted));
        OnPropertyChanged(nameof(TotalFormatted));
    }

    public string CompleteCheckout() // ← renamed from Checkout()
    {
        var receipt = ShoppingCartService.Current.Checkout();
        RefreshCart(); // clear UI cart after checkout
        return receipt;
    }

    private void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}