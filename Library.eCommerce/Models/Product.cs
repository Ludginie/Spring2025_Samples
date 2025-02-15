using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring2025_Samples.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; } //a property, give public access

        public string? Display //bind object to string representation
        {
            get
            {
                return $"{Id}. {Name}";
            }
            
        }
        public Product()//default construct
        {
           Name = string.Empty; 
        }

        public override string ToString()
        {
            return Display ?? string.Empty; //if it is null make it empty
        }
    }
}