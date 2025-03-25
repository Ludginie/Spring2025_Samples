using Library.eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class ShoppingCartService
    {
        private ProductServiceProxy _prodSvc;
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
            _prodSvc = ProductServiceProxy.Current;
        }
        
        public bool AddToCart(int productId, int quantity)//adding an item to the cart
        {
            var product = _prodSvc.GetById(productId);
            if (product != null && product.Quantity >= quantity)
            {
                var existingItem = items.FirstOrDefault(i => i.Product.Id == productId);
                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    items.Add(new Item { Product = product.Product, Quantity = quantity });
                }

                product.Quantity -= quantity;
                return true;
            }
            return false;
        }//end of add to cart
        
        public bool UpdateCart(int productId, int newQuantity)//updating an item quantity
        {
            var cartItem = items.FirstOrDefault(i => i.Product.Id == productId);
            if (cartItem != null && newQuantity >= 0)
            {
                int oldQuantity = cartItem.Quantity ?? 0;
                int difference = newQuantity - oldQuantity;

                var inventoryItem = _prodSvc.GetById(productId);
                if (inventoryItem != null && inventoryItem.Quantity >= difference)
                {
                    inventoryItem.Quantity -= difference;
                    cartItem.Quantity = newQuantity;
                    return true;
                }
            }
            return false;
        }//end of update cart
        
        public bool RemoveFromCart(int productId)//returning stock to inventory
        {
            var item = items.FirstOrDefault(i => i.Product.Id == productId);
            if (item != null)
            {
                var inventoryItem = _prodSvc.GetById(productId);
                if (inventoryItem != null)
                {
                    inventoryItem.Quantity += item.Quantity ?? 0; // Return stock
                }
                items.Remove(item);
                return true;
            }
            return false;
        }//end of removefromcart function

        
        public string Checkout()//checking out  w the tax and assuming each item are 2 dollars
        {
            if (items.Count == 0)
            {
                return "the cart is empty.";
            }
            string recus = "Receipt:";
            double total = 0;
            foreach (var item in items)
            { double itemTotal = (item.Quantity ?? 0) * 2.00;//we are setting the price of everything to 2 dollars
                total += itemTotal;
                recus += $"{item.Product.Name} : {item.Quantity} - ${itemTotal:0.00}\n";
            }
            double tax = total * 0.07;
            double leGrandTotal = total + tax;
            recus += $"Subtotal: ${total:0.00}\n";
            recus += $"Tax: ${tax:0.00}\n";
            recus += $"Total: ${leGrandTotal:0.00}\n";
            items.Clear();
            return recus;
        }//end fo checkout function
    }
}
