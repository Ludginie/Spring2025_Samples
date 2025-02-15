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
            
        }
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
        
        private List<Product?> list = new List<Product?>();

        public List<Product?> Products => list;
    }//end of the class
}//end of namespace

