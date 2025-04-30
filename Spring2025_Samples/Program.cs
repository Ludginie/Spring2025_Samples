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
            Console.WriteLine("Welcome to Amazon!");

            Console.WriteLine("C. Create new inventory item");
            Console.WriteLine("R. Read all inventory items");
            Console.WriteLine("U. Update an inventory item");
            Console.WriteLine("D. Delete an inventory item");
            Console.WriteLine("Q. Quit");

            List<Item?> list = ProductServiceProxy.Current.Products;

            char choice;
            do
            {
                string? input = Console.ReadLine();
                choice = input[0];

                switch (choice)
                {
                    case 'C':
                    case 'c':
                        Console.Write("Enter product name: ");
                        string name = Console.ReadLine() ?? "Unnamed Product";
                        ProductServiceProxy.Current.AddOrUpdate(new Item
                        {
                            Product = new Library.eCommerce.DTO.ProductDTO { Name = name },
                            Quantity = 0
                        });

                        break;

                    case 'R':
                    case 'r':

                        list.ForEach(item => Console.WriteLine(item?.Product?.Name));
                        break;

                    case 'U':
                    case 'u':
                        Console.WriteLine("Which product would you like to update?");
                        int selection = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedItem = list.FirstOrDefault(p => p?.Id == selection);

                        if (selectedItem != null)
                        {
                            Console.Write("Enter new product name: ");
                            selectedItem.Product.Name = Console.ReadLine() ?? "ERROR";
                            ProductServiceProxy.Current.AddOrUpdate(selectedItem);
                        }
                        break;

                    case 'D':
                    case 'd':
                        Console.WriteLine("Which product would you like to delete?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        ProductServiceProxy.Current.Delete(selection);
                        break;

                    case 'Q':
                    case 'q':
                        break;

                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }
            } while (choice != 'Q' && choice != 'q');

            Console.ReadLine();
        }
    }
}
