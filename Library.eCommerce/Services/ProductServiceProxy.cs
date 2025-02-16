using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring2025_Samples.Models;

namespace Library.eCommerce.Services
{
    public class ProductServiceProxy
    {
        private ProductServiceProxy()
        {
            Products = new List<Product>();
        }

        private int LastKey
        {
            get
            {
                if (!Products.Any())
                {
                    return 0;
                }//end of if statement

                return Products.Select (p=>p?.Id ?? 0 ).Max();
            }//end of get 
        }//end of last key

        private static ProductServiceProxy? instance;//instance on the type
        private static object instanceLock = new object();
        public static ProductServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductServiceProxy();
                    }
                }
                return instance;
            }//end of the getter
        }//end of current product service proxy
        
      

        public List<Product?> Products { get; private set; }
    
        public Product AddOrUpdate(Product product)
        {
            if (product.Id == 0)
            {
                product.Id = LastKey + 1;
                Products.Add(product); 
            }
            
            return product;
        }//end of add

        public Product Delete(int id)
        {
            if (id == 0)
            {
                return null;
            }
            Product? product = Products.FirstOrDefault(p => p.Id == id);//shallow copy
            Products.Remove(product);
            return product;
        }
        
    }//end of the class
}//end of namespace

