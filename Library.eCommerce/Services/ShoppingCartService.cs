using Spring2025_Samples.Models;
using Library.eCommerce.Models;
using Library.eCommerce.DTO;

namespace Library.eCommerce.Services
{
    public class ShoppingCartService
    {
        private ProductServiceProxy _prodSvc = ProductServiceProxy.Current;
        private List<Item> items;

        public List<Item> CartItems => items;

        public static ShoppingCartService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShoppingCartService();
                }
                return instance;
            }
        }

        private static ShoppingCartService? instance;

        private ShoppingCartService()
        {
            items = new List<Item>();
        }

        public Item? AddOrUpdate(Item item)
        {
            var existingInvItem = _prodSvc.GetById(item.Id);
            if (existingInvItem == null || existingInvItem.Quantity == 0)
            {
                return null;
            }

            existingInvItem.Quantity--;

            var existingItem = CartItems.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem == null)
            {
                // Add new item, copying data manually into a ProductDTO
                var newItem = new Item
                {
                    Id = item.Id,
                    Quantity = 1,
                    Product = new ProductDTO
                    {
                        Id = existingInvItem.Product?.Id ?? 0,
                        Name = existingInvItem.Product?.Name ?? "Unknown",
                        Price = existingInvItem.Product?.Price ?? 2.00
                    }
                };
                CartItems.Add(newItem);
            }
            else
            {
                existingItem.Quantity++;
            }

            return existingInvItem;
        }


        public Item? ReturnItem(Item? item)
        {
            if (item?.Id <= 0 || item == null)
            {
                return null;
            }
            var itemToReturn = CartItems.FirstOrDefault(c => c.Id == item.Id);
            if (itemToReturn != null)
            {
                itemToReturn.Quantity--;
                var inventoryItem = _prodSvc.Products.FirstOrDefault(p => p.Id == itemToReturn.Id);
                if (inventoryItem == null)
                {
                    _prodSvc.AddOrUpdate(new Item(itemToReturn));
                }
                else
                {
                    inventoryItem.Quantity++;
                }
            }

            return itemToReturn;
        }//end of return item

        public string Checkout()
        {
            if (CartItems.Count == 0)
            {
                return "The cart is empty.";
            }

            string receipt = "Receipt:\n";
            double total = 0;

            foreach (var item in CartItems)
            {
                double price = item.Product?.Price ?? 2.00;//setting each of the products to $2
                double itemTotal = (item.Quantity ?? 0) * price;
                receipt += $"{item.Product?.Name} x {item.Quantity} - ${itemTotal:F2}\n";
                total += itemTotal;
            }

            double tax = total * 0.07;
            double grandTotal = total + tax;

            receipt += $"Subtotal: ${total:F2}\n";
            receipt += $"Tax (7%): ${tax:F2}\n";
            receipt += $"Total: ${grandTotal:F2}";

            CartItems.Clear();
            return receipt;
        }//for the reciept
    }
}
