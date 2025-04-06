using System.Collections.ObjectModel;
using System.ComponentModel;
using Library.eCommerce.Models;

namespace Maui.eCommerce.ViewModels;

public class CheckoutViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Item> CartItems { get; set; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    public CheckoutViewModel()
    {
        // Simulate cart data (replace this with your real service call)
        CartItems.Add(new Item { Product = new() { Name = "Shirt", Price = 20.00 }, Quantity = 1 });
        CartItems.Add(new Item { Product = new() { Name = "Jeans", Price = 40.00 }, Quantity = 1 });
    }

    public double Subtotal => CartItems.Sum(item => item.Product.Price);
    public double Tax => Subtotal * 0.07;
    public double Total => Subtotal + Tax;

    public string SubtotalFormatted => $"Subtotal: ${Subtotal:F2}";
    public string TaxFormatted => $"Tax (7%): ${Tax:F2}";
    public string TotalFormatted => $"Total: ${Total:F2}";
}