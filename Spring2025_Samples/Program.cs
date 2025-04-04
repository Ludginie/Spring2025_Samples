﻿using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Xml.Serialization;

namespace Spring2025_Samples
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

            List<Product?> list = ProductServiceProxy.Current.Products;//equal to exactly the list of products in service proxy
            //above is a shallow copy

            char choice;
            do
            {
                string? input = Console.ReadLine();
                choice = input[0];
                switch (choice)
                {
                    case 'C':
                    case 'c':
                        ProductServiceProxy.Current.AddOrUpdate(new Product
                        {
                            Name = Console.ReadLine()
                        });
                        break;
                    case 'R':
                    case 'r':
                        //print out all the products in the list
                        list.ForEach(Console.WriteLine);
                        break;
                    case 'U':
                    case 'u':
                        //select one of the products, replace with new one
                        Console.WriteLine("Which product would you like to update?");
                        int selection = int.Parse(Console.ReadLine()?? "-1");
                        var selectedProduct = list.FirstOrDefault(p => p.Id == selection);//go thru list and find prod w id same as selection
                        
                        if (selectedProduct != null)
                        {
                            selectedProduct.Name = Console.ReadLine() ?? "ERROR";//if selection dne
                            ProductServiceProxy.Current.AddOrUpdate(selectedProduct);
                        }

                        break;
                    case 'D':
                    case 'd':
                        //select one of the products
                        //throw it away
                        Console.WriteLine("Which product would you like to Delete?");
                        selection = int.Parse(Console.ReadLine()?? "-1");
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
           
        }
    }
}//end of spring 2025 samples