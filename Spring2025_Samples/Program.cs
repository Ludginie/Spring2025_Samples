using Library.eCommerce.Models;
using Library.eCommerce.Services;
using Spring2025_Samples.Models;


using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cartService = ShoppingCartService.Current;
            var productService = ProductServiceProxy.Current;
            Console.WriteLine("\nWelcome to Amazon!");
            char choice;
            do
            {
                
                Console.WriteLine("\nC. Create new inventory item");
                Console.WriteLine("R. Read all inventory items");
                Console.WriteLine("U. Update an inventory item");
                Console.WriteLine("D. Delete an inventory item");
                Console.WriteLine("A. Add item to shopping cart");
                Console.WriteLine("B. Read shopping cart");
                Console.WriteLine("E. Remove item from cart");
                Console.WriteLine("F. Modify an item's quantity in the cart");
                Console.WriteLine("G. Checkout");
                Console.WriteLine("Q. Quit");

                string? input = Console.ReadLine();
                choice = (input != null && input.Length > 0) ? char.ToUpper(input[0]) : ' ';
                Console.WriteLine("DEBUGGING: YOU ENTERED '" + choice + "'");
                switch (choice)
                {
                    case 'C':
                        Console.Write("Product name: ");
                        string productName = Console.ReadLine() ?? "Unnamed Product";
                        var newItem = new Item { Product = new Product { Name = productName }, Quantity = 0 };
                        productService.AddOrUpdate(newItem);
                        Console.WriteLine("Product added!");
                        break;

                    case 'R':
                        Console.WriteLine("\nInventory:");
                        productService.Products.ForEach(Console.WriteLine);

                        break;

                    case 'U':
                        Console.Write("Enter the product's ID that you would like to update: ");
                        if (int.TryParse(Console.ReadLine(), out int updateId))
                        {
                            var selectedProd = productService.GetById(updateId);
                            if (selectedProd != null)
                            {
                                Console.Write("The new name: ");
                                selectedProd.Product.Name = Console.ReadLine() ?? "ERROR";
                                productService.AddOrUpdate(selectedProd);
                            }
                            else
                            {
                                Console.WriteLine("No product with this ID!");
                            }
                        }
                        break;

                    case 'D':
                        Console.Write("Enter the product's ID that you would like to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            productService.Delete(deleteId);
                        }
                        break;

                    case 'A':
                        Console.Write("Enter product ID that you would like to add: ");
                        if (int.TryParse(Console.ReadLine(), out int addId))
                        {
                            Console.Write("How many (make sure it is =< than what is in inventory: ");
                            if (int.TryParse(Console.ReadLine(), out int qty))
                            {
                                if (cartService.AddToCart(addId, qty))
                                    Console.WriteLine("The item was added to your cart!");
                                else
                                    Console.WriteLine("Not enough in the inventory.");
                            }
                        }
                        break;

                    case 'B':
                        Console.WriteLine("\nShopping Cart:");
                        foreach (var cartItem in cartService.CartItems)
                        {
                            Console.WriteLine($"{cartItem.Product.Name} X {cartItem.Quantity}");
                        }
                        break;

                    case 'E':
                        Console.Write("Enter product's ID that you would like to remove from the cart : ");
                        if (int.TryParse(Console.ReadLine(), out int removeId))
                        {
                            if (cartService.RemoveFromCart(removeId))
                                Console.WriteLine("Item returned to inventory and removed from cart!");
                            else
                                Console.WriteLine("Item not found in cart.");
                        }
                        break;

                    case 'F':
                        Console.Write("Enter product ID to modify in cart: ");
                        if (int.TryParse(Console.ReadLine(), out int modifyId))
                        {
                            Console.Write("Enter new quantity: ");
                            if (int.TryParse(Console.ReadLine(), out int newQty))
                            {
                                if (cartService.UpdateCart(modifyId, newQty))
                                    Console.WriteLine("Your cart was updated!");
                                else
                                    Console.WriteLine("Error: Invalid quantity or product not found.");
                            }
                        }
                        break;

                    case 'G':
                        Console.WriteLine("\n" + cartService.Checkout());
                        break;
                    
                    case 'Q':
                    case 'q':
                        break;
                    
                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }


            } while (choice != 'Q' && choice != 'q');
        }
    }
}
